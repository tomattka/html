from .models import YgUser, YgUserInfo
from django.templatetags.static import static
from django.db import connection
from django.urls import reverse
from userConfig.models import YgUserConfig
from django.forms.models import model_to_dict


def get_tags_sql(tag_type, user_id): # tag_type = 'doctrines', 'traditions', 'practices' or 'interests'
    cursor = connection.cursor()

    # SQL queries are used because of django's bad M2M order support
    if tag_type == "doctrines":
        query = f'''select d.id, d.title from userProfile_yguserinfo_doctrines ui
                    left outer join userProfile_ygdoctrine d on ui.ygdoctrine_id=d.id
                    where yguserinfo_id={user_id}
                    order by ui.id'''

    if tag_type == "traditions":
        query = f'''select t.id, t.title from userProfile_yguserinfo_traditions ui
                    left outer join userProfile_ygtradition t on ui.ygtradition_id=t.id
                    where yguserinfo_id={user_id}
                    order by ui.id'''

    if tag_type == "practices":
        query = f'''select p.id, p.title from userProfile_yguserinfo_practices ui
                    left outer join userProfile_ygpractice p on ui.ygpractice_id=p.id
                    where yguserinfo_id={user_id}
                    order by ui.id'''

    if tag_type == "interests":
        query = f'''select i.id, i.title from userProfile_yguserinfo_interests ui
                    left outer join userProfile_yginterest i on ui.yginterest_id=i.id
                    where yguserinfo_id={user_id}
                    order by ui.id'''

    try:
        cursor.execute(query)
        return cursor
    except:
        return None


def get_tags_lists(cursor, tag_type='tag'): # type = doctrine, tradition, practice or interest
    #  returns links and tag formated list from cursor contained ids and titles
    list = tags = ''
    link_search = reverse('user_search')
    if cursor:
        for item in cursor:
            list += f'<a href="{link_search}?{tag_type}={item[1]}">{item[1]}</a>, '
            tags += f'<div class="tags__item">{item[1]}<button tabindex="-1"><img src="' + static(
                'controls/img/tag_close.png') + '" alt="Закрыть"></button></div> '
        list = list.removesuffix(', ')
    return list, tags


def getUser(user_id):
    main_info = {}
    if YgUser.objects.filter(id=user_id):
        main_info = YgUser.objects.get(id=user_id)
    return main_info


def getUserData(user_id, user_gender):
    user_info = {}
    # if YgUserInfo.objects.filter(user_id=user_id):
    user_info, created = YgUserInfo.objects.get_or_create(user_id=user_id)

    # Учение
    cur_doctrines = get_tags_sql("doctrines", user_id)
    user_info.doctrine_list, user_info.doctrine_tags = get_tags_lists(cur_doctrines, 'doctrine')

    # Традиция
    cur_traditions = get_tags_sql("traditions", user_id)
    user_info.tradition_list, user_info.tradition_tags = get_tags_lists(cur_traditions, 'tradition')

    # Практики
    cur_practices = get_tags_sql("practices", user_id)
    user_info.practice_list, user_info.practice_tags = get_tags_lists(cur_practices, 'practice')

    # Интересы
    cur_interests = get_tags_sql("interests", user_id)
    user_info.interest_list, user_info.interest_tags = get_tags_lists(cur_interests, 'interest')

    # Выводим семейное положение с учетом пола
    if user_info.marital_status:
        user_info.str_mar_status = str(user_info.marital_status)
        if str(user_gender) == 'женский':  # user_gender = request.user.gender
            user_info.str_mar_status = user_info.marital_status.title_fem

    # About and request with replacements
    if user_info.about:
        user_info.about_txt = user_info.about.replace("<br />", "\n")
    else:
        user_info.about = ""

    if user_info.request:
        user_info.request_txt = user_info.request.replace("<br />", "\n")
    else:
        user_info.request = ""

    return user_info


def getVocabularies(user_gender='нейтральный'):
    vocabularies = {"statusList": ""}

    # marital status
    from .models import YgMaritalStatus
    statuses = YgMaritalStatus.objects.all().order_by('order')
    vocabularies["statusList"] = '<a href="#">не указано</a>'
    for status in statuses:
        if str(user_gender) == 'женский':
            vocabularies["statusList"] += '<a href="#">' + status.title_fem + '</a>'
        elif str(user_gender) == 'нейтральный':
            vocabularies["statusList"] += '<a href="#">' + status.title_neutral + '</a>'
        else:
            vocabularies["statusList"] += '<a href="#">' + status.title + '</a>'

    # gender
    from .models import YgGender
    genders = YgGender.objects.all().order_by('order')
    vocabularies["genders"] = '<a href="#">не указано</a>'
    for gender in genders:
        vocabularies["genders"] += '<a href="#">' + gender.title + '</a>'

    # experience
    from .models import YgExperience
    experience = YgExperience.objects.all().order_by('order')
    vocabularies["experience"] = '<a href="#">не указано</a>'
    for exp in experience:
        vocabularies["experience"] += '<a href="#">' + exp.title + '</a>'

    return vocabularies


def getUserPhotos(user_id):
    result = ""

    from photos.models import YgUserPhoto
    user_photos = YgUserPhoto.objects.filter(user=user_id).order_by('-order', '-id')

    for photo in user_photos:
        result += f'<a href="{photo.photo_big.url}" data-fancybox="gallery" target="_blank" class="user-view__prevs-link">' \
                  f'<img class="user-view__prevs-img" src="{photo.photo_small.url}" ' \
                  f'alt="{photo.user.first_name} {photo.user.last_name}"></a>'
    return result


def get_noindex_tags(user_id):
    user_config, created = YgUserConfig.objects.get_or_create(user_id=user_id)
    noindex = {'start': '<noindex>', 'end': '</noindex>', 'rel': 'rel="nofollow"'} if user_config.se_forbidden \
        else {'start': '', 'end': '', 'rel': ''}
    return noindex


def is_enough_info(user_info, min_amount=2):
    info_dict = model_to_dict(user_info)
    val_count = 0
    for val in info_dict.values():
        if (val):
            val_count += 1
    # print(val_count)
    if val_count < min_amount:
        return False
    else:
        return True
