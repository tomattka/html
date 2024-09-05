from django.shortcuts import render
from django.templatetags.static import static
from django.urls import reverse
from django.views.generic.base import View
from userProfile.models import YgUser, YgUserInfo
from userProfile.forms import YGLoginForm
from userProfile.functions import get_noindex_tags, get_tags_sql
from search.functions import get_age_and_city


def get_user_tags(user):
    # Getting practices
    tag_type = 'practice'
    tags_cursor = get_tags_sql("practices", user.id)

    # Getting traditions
    if tags_cursor.rowcount < 1:
        tag_type = 'tradition'
        tags_cursor = get_tags_sql("traditions", user.id)

    # Getting doctrines
    if tags_cursor.rowcount < 1:
        tag_type = 'doctrine'
        tags_cursor = get_tags_sql("doctrines", user.id)

    # Getting interests
    if tags_cursor.rowcount < 1:
        tag_type = 'interest'
        tags_cursor = get_tags_sql("interests", user.id)

    # if has any tags
    if tags_cursor.rowcount > 0:
        tags_html = ''
        link_search = reverse('user_search')

        i = 0
        if tags_cursor:
            for item in tags_cursor:
                if i < 2:
                    tags_html += f'<a href="{link_search}?{tag_type}={item[1]}">{item[1]}</a>'
                i += 1
    else:
        tags_html = ''

        if YgUserInfo.objects.filter(user=user):
            user_info = YgUserInfo.objects.get(user=user)
            if user_info.about:
                tags_html = f'<b>О себе:</b>&nbsp;{user_info.about}'
            elif user_info.request:
                tags_html = f'<b>Ищу:</b>&nbsp;{user_info.request}'
            else:
                tags_html = '<span style="color: #8A8A9E; padding-bottom: 2px;">Интересы не указаны</span>'
        # Request

    return tags_html

def get_users_html(users):
    result = ''
    for user in users:
        photo_url = static('img/user-profile/no-photo.png')
        if user.profile_pic:
            photo_url = user.profile_pic.url

        age_and_city = get_age_and_city(user.id)

        user_url = reverse('user_profile', kwargs={'user_id': user.id})
        # noindex = get_noindex_tags(user.id)

        tags_html = get_user_tags(user)

        result += f'<div class="col-sm-12 col-md-6 col-lg-4 col-xl-4 d-flex justify-content-center justify-content-md-start justify-content-lg-start justify-content-xl-start">' \
                  f'<div class="pinkbg">' \
                  f'' \
                  f'<a href="{user_url}"><img src="{photo_url}" alt="{user.first_name} {user.last_name}" class="avatar" /></a>' \
                  f'<p class="name"><a href="{user_url}">{user.first_name}<br>{user.last_name}</a></p>' \
                  f'<p class="about">{age_and_city}</p>' \
                  f'<div class="interests">{tags_html}</div>' \
                  f'</div></div>'
    return result


class MainPage(View):
    def get(self, request):
        users = YgUser.objects.order_by("-id")[:6]
        users_html = get_users_html(users)
        return render(request, "ygTemplates/tplMain.html", {"recent_users": users_html, "form": YGLoginForm})
