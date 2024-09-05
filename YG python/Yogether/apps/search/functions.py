from datetime import date, timedelta
from django.templatetags.static import static
from django.urls import reverse
from django.db.models import Q, Value
from django.db.models.functions import Concat
from userProfile.models import YgUser, YgUserInfo
from userProfile.functions import getVocabularies, get_noindex_tags
import re


def get_age_and_city(user_id):
    age_and_city = ''

    if YgUserInfo.objects.filter(user_id=user_id):
        user_info = YgUserInfo.objects.get(user_id=user_id)
        age_and_city = user_info.getAge()
        if age_and_city and user_info.location:
            age_and_city += ', '
        if user_info.location:
            age_and_city += f'<span>{user_info.location}</span>'

    return age_and_city


def search_result_html(users):
    result = ''
    users_ids = []

    for user in users:

        if user.id not in users_ids:
            photo_url = static('img/user-profile/no-photo.png')
            if user.profile_pic:
                photo_url = user.profile_pic.url

            age_and_city = get_age_and_city(user.id)

            user_url = reverse('user_profile', kwargs={'user_id': user.id})
            noindex = get_noindex_tags(user.id)

            result += f'{noindex["start"]}<a href="{user_url}" {noindex["rel"]} class="search__item item">' \
                      f' <div class="item__pic"><img src="{photo_url}" alt="{user.first_name} {user.last_name}" /></div>' \
                      ' <div class="item__info">' \
                      f'     <div class="item__name">{user.first_name} <br>{user.last_name}</div>' \
                      f'     <div class="item__desc">{age_and_city}</div>' \
                      ' </div>' \
                      f'</a>{noindex["end"]}'

        users_ids.append(user.id)

    if not result:
        result = '''<div class="search__not-found">Ничего не найдено.<br>
                    Попробуйте расширить условия поиска.</div>'''

    return result


def get_users_by_params(params):
    users = YgUser.objects.filter(~Q(yguserconfig__profile_open=False)) # excludes either when no user confidg
    # !!!!!!! разобраться с параметрами и индексом

    # Location
    location = params[0]['iLocation']
    if location:
        users = users.filter(yguserinfo__location__title__icontains=location)

    # Age
    days_in_year = 365.25

    # Age from
    age_from = params[1]['iAgeFrom']

    try:
        age_from = int(age_from)
    except ValueError:
        age_from = 0

    if age_from != 0:
        date_till = date.today() - timedelta(days= age_from * days_in_year - 1)
        users = users.filter(yguserinfo__birth_date__lte=date_till)

    # Age till
    age_till = params[2]['iAgeTill']

    try:
        age_till = int(age_till)
    except ValueError:
        age_till = 0

    if age_till != 0:
        date_from = date.today() - timedelta(days= (age_till + 1) * days_in_year + 1)
        users = users.filter(yguserinfo__birth_date__gte=date_from)

    # Gender
    gender = params[3]['iGender']
    if gender and gender != 'любой':
        if gender == 'не указано':
            users = users.filter(gender=None)
        else:
            users = users.filter(gender__title__icontains=gender)

    # Marital status
    marital = params[4]['iMarital']
    if marital and marital != 'любой':
        if marital == 'не указано':
            users = users.filter(yguserinfo__marital_status=None)
        else:
            users = users.filter(yguserinfo__marital_status__title_neutral=marital)

    # Experience
    experience = params[5]['iExperience']
    if experience and experience != 'любой':
        if experience == 'не указано':
            users = users.filter(yguserinfo__experience=None)
        else:
            users = users.filter(yguserinfo__experience__title=experience)


    # -------- Right part -----------
    # Doctrine
    doctrines = params[6]['tagsDoctrine']
    if doctrines:
        doctrine_list = doctrines.split(",")
        users = users.filter(yguserinfo__doctrines__title__in=doctrine_list)

    # Traditions
    traditions = params[7]['tagsTradition']
    if traditions:
        tradition_list = traditions.split(",")
        users = users.filter(yguserinfo__traditions__title__in=tradition_list)

    # Practices
    practices = params[8]['tagsPractice']
    if practices:
        practice_list = practices.split(",")
        users = users.filter(yguserinfo__practices__title__in=practice_list)

    # Interests
    interests = params[9]['tagsInterests']
    if interests:
        interest_list = interests.split(",")
        users = users.filter(yguserinfo__interests__title__in=interest_list)

    return users


def get_users_by_text(search_value, search_order, users_by_params):
    users = users_by_params.annotate(full_name=Concat('first_name', Value(' '), 'last_name'),
                                    search_order=Value(search_order)) \
        .filter(Q(yguserinfo__about__icontains=search_value)
                | Q(yguserinfo__request__icontains=search_value)
                | Q(first_name__icontains=search_value)
                | Q(last_name__icontains=search_value)
                | Q(full_name__icontains=search_value)
                | Q(yguserinfo__location__title__icontains=search_value)
                | Q(yguserinfo__marital_status__title__icontains=search_value)
                | Q(yguserinfo__marital_status__title_fem__icontains=search_value)
                | Q(yguserinfo__doctrines__title__icontains=search_value)
                | Q(yguserinfo__practices__title__icontains=search_value)
                | Q(yguserinfo__traditions__title__icontains=search_value)
                | Q(yguserinfo__interests__title__icontains=search_value)) \
        .distinct()
    return users


def do_search(search_text, params):
    # Parameters
    users_by_params = get_users_by_params(params)

    # Search by full text
    users = get_users_by_text(search_text, 1, users_by_params)

    # Search by words
    words = re.findall(r"[\w']+", search_text)
    for word in words:
        users_by_word = get_users_by_text(word, 2, users_by_params)
        users = users | users_by_word  # !!! Initially it was "union", caused error migrated to mysql; may work wrongly

    users = users.order_by('search_order', '-id')

    # Convert to html
    result = search_result_html(users)
    return result