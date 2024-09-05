from datetime import timedelta
from django.db.models import Q
from django.urls import reverse
from django.templatetags.static import static
import random
from userProfile.models import YgUser, YgUserInfo
from userProfile.functions import get_noindex_tags
from search.functions import get_age_and_city
from .models import YgRecommendRemove


def recommend_result_html(users, viewer_id):
    result = ''

    for user in users:

        photo_url = static('img/user-profile/no-photo.png')
        if user.profile_pic:
            photo_url = user.profile_pic.url

        age_and_city = get_age_and_city(user.id)

        user_url = reverse('user_profile', kwargs={'user_id': user.id})
        noindex = get_noindex_tags(user.id)

        result += f'{noindex["start"]}<a id="user_{user.id}" href="{user_url}" {noindex["rel"]} class="search__item item">'

        if viewer_id:
            result += f'<div class="item__delete"><img src="{static("img/popups/close.png")}" alt="Х"></div>'

        result += f' <div class="item__pic"><img src="{photo_url}" alt="{user.first_name} {user.last_name}" /></div>' \
                  ' <div class="item__info">' \
                  f'     <div class="item__name">{user.first_name} <br>{user.last_name}</div>' \
                  f'     <div class="item__desc">{age_and_city}</div>' \
                  ' </div>' \
                  f'</a>{noindex["end"]}'

    return result


def get_random_users(full_ids_list, amount=3):
    random_users = set()

    if len(set(full_ids_list)) > amount:
        while len(random_users) < amount:
            random_user = random.choice(full_ids_list)
            random_users.add(random_user)
    else:
        random_users = set(full_ids_list)
    return random_users


def append_recommended_users(users, recommendations, koefs, koef=1):
    for u in users:
        recommendations.append(u.user_id)
        koefs.append(koef)


def get_recommendations(user_id, viewer_id=0):  # returns recommendation html or empty if error
    # try:
    recommendations = []
    koefs = []

    # hidden from search
    user_hidden = list(YgUserInfo.objects.filter(user__yguserconfig__profile_open=False).values_list('user_id', flat=True))

    #removed from reccomendations
    users_removed = list(YgRecommendRemove.objects.filter(user_self=viewer_id).values_list('user_remove', flat=True)) if viewer_id != 0 else []

    user_infos = YgUserInfo.objects.filter(~Q(user_id__in=(user_id, viewer_id)) & ~Q(user_id__in=user_hidden) & ~Q(user_id__in=users_removed))\
        .order_by("-user_id")  # excluding user himself

    user_info = YgUserInfo.objects.get(user_id=user_id)

    # users with the same tradition
    user_traditions = user_info.traditions.all()
    if user_traditions:
        # DISTINCT is not used for more repeated users get more koefs
        users_same_traditions = user_infos.filter(traditions__title__in=list(user_traditions))
        append_recommended_users(users_same_traditions, recommendations, koefs, 4)

    # users with the same location
    user_location = user_info.location
    if user_location:
        users_same_locations = user_infos.filter(location=user_location)
        append_recommended_users(users_same_locations, recommendations, koefs, 3)

    # users with the same doctrine
    user_doctrines = user_info.doctrines.all()
    if user_doctrines:
        users_same_doctrines = user_infos.filter(doctrines__title__in=list(user_doctrines))
        append_recommended_users(users_same_doctrines, recommendations, koefs, 2)

    # users with the same practices
    user_practices = user_info.practices.all()
    if user_practices:
        users_same_practices = user_infos.filter(practices__title__in=list(user_practices))
        append_recommended_users(users_same_practices, recommendations, koefs, 1)

    # users with the same interests
    user_interests = user_info.interests.all()
    if user_interests:
        users_same_interests = user_infos.filter(interests__title__in=list(user_interests))
        append_recommended_users(users_same_interests, recommendations, koefs, 1)

    # если меньше трех, добавляем пользователей +- 5 лет (ограничено последними 50, новые сверху определены вначале)
    if len(set(recommendations)) < 3 and user_info.birth_date:
        days_in_year = 365.25
        date_from = user_info.birth_date - timedelta(days=5 * days_in_year)
        date_till = user_info.birth_date + timedelta(days=5 * days_in_year)
        users_similar_age = user_infos.filter(birth_date__lte=date_till, birth_date__gte=date_from)[:50]
        append_recommended_users(users_similar_age, recommendations, koefs, 1)

    # if still not enough users, add last 50 to random list
    if len(set(recommendations)) < 3:
        other_users = user_infos[:50]
        append_recommended_users(other_users, recommendations, koefs, 1)

    # getting full list id for random
    full_ids_list = []
    for i, user_id in enumerate(recommendations):
        koef = koefs[i]
        for x in range(koef):
            full_ids_list.append(user_id)

    # getting random 3 users
    random_users_ids = get_random_users(full_ids_list)

    users_to_show = YgUser.objects.filter(id__in=random_users_ids)
    result_html = recommend_result_html(users_to_show, viewer_id)

    # except:
    #     result_html = ''

    return result_html