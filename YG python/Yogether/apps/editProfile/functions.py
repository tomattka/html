from userProfile.models import YgUserInfo
from django.db.models import Q
from django.utils.dateparse import parse_date


def getUserInfo(user_id):
    user_info = YgUserInfo(user_id=user_id)  # creating
    if YgUserInfo.objects.filter(user_id=user_id):
        user_info = YgUserInfo.objects.filter(user_id=user_id)[0]  # or getting
    return user_info


def get_location(value):
    from userProfile.models import YgLocation
    yg_location = None
    if value != "":
        try:
            yg_location = YgLocation.objects.get(title__iexact=value)  # case insensitive
        except YgLocation.DoesNotExist:
            yg_location = YgLocation(title=value)
            yg_location.save()
    return yg_location


def get_birth_date(value):
    try:
        birthday = parse_date(value)
    except:
        birthday = None
    return birthday


def get_gender(value):
    from userProfile.models import YgGender
    try:
        gender = YgGender.objects.get(title__iexact=value)
    except YgGender.DoesNotExist:
        gender = None
    return gender


def get_marital_status(value):
    from userProfile.models import YgMaritalStatus
    try:
        marital_status = YgMaritalStatus.objects.get(Q(title__iexact=value) | Q(title_fem__iexact=value))
    except YgMaritalStatus.DoesNotExist:
        marital_status = None
    return  marital_status


def get_experience(value):
    from userProfile.models import YgExperience
    try:
        experience = YgExperience.objects.get(title__iexact=value)
    except YgExperience.DoesNotExist:
        experience = None
    return experience


def set_user_doctrines(user_info, value):
    user_info.doctrines.clear()
    if value:
        values = value.split(",")
        from userProfile.models import YgDoctrine
        for item in values:
            try:
                doctrine = YgDoctrine.objects.get(title__iexact=item)
            except:
                doctrine = YgDoctrine(title=item)
                doctrine.save()
            user_info.doctrines.add(doctrine)


def set_user_tradition(user_info, value):
    user_info.traditions.clear()
    if value:
        values = value.split(",")
        from userProfile.models import YgTradition
        for item in values:
            try:
                tradition = YgTradition.objects.get(title__iexact=item)
            except:
                tradition = YgTradition(title=item)
                tradition.save()
            user_info.traditions.add(tradition)


def set_user_practices(user_info, value):
    user_info.practices.clear()
    if value:
        values = value.split(",")
        from userProfile.models import YgPractice
        for item in values:
            try:
                practice = YgPractice.objects.get(title__iexact=item)
            except:
                practice = YgPractice(title=item)
                practice.save()
            user_info.practices.add(practice)


def set_user_interests(user_info, value):
    user_info.interests.clear()
    if value:
        values = value.split(",")
        from userProfile.models import YgInterest
        for item in values:
            try:
                interest = YgInterest.objects.get(title__iexact=item)
            except:
                interest = YgInterest(title=item)
                interest.save()
            user_info.interests.add(interest)
