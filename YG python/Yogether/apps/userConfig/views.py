from django.shortcuts import render
from django.views.generic.base import View
from .models import YgUserConfig, YgTimezone, YgUserNotification

NOTIF_FREQ = {
    "Всегда": 999,
    "Никогда": 0,
    "Раз в час": 1,
    "Раз в 3 часа": 3,
    "Раз в день": 24
}
MENU_LINKS = {"config": "profile-menu__item_selected"}

def get_config_data(user):
    user_data = {}
    user_data["menu_links"] = {"config": "profile-menu__item_selected"}

    user_config, created = YgUserConfig.objects.get_or_create(user=user)

    checkboxes = {}
    if user_config.profile_open:
        checkboxes["profile_open"] = 'checked'
    if user_config.se_forbidden:
        checkboxes["se_forbidden"] = 'checked'
    if user_config.get_news:
        checkboxes["get_news"] = 'checked'

    user_data["checkboxes"] = checkboxes

    user_data["time_zone"] = user_config.time_zone.title

    notification, created = YgUserNotification.objects.get_or_create(user=user)
    freq = notification.notification_freq
    user_data["notif_freq"] = list(NOTIF_FREQ.keys())[list(NOTIF_FREQ.values()).index(freq)]  # get key by value from dict NOTIF_FREQ

    return user_data


def get_config_vocabularies():
    vocabularies = {"time_zones": ""}
    time_zones = YgTimezone.objects.all()
    for zone in time_zones:
        vocabularies["time_zones"] += '<a href="#">' + zone.title + '</a>'
    return vocabularies


class UserConfig(View):
    def get(self, request):
        user_data = get_config_data(request.user)
        vocabularies = get_config_vocabularies()
        return render(request, "ygProfile/user-config.html", {"vocabularies": vocabularies, "user_data": user_data,
                                                              "menu_links": MENU_LINKS})

    def post(self, request):
        # getting checkboxes
        CHECKBOX_MAPPING = {'on': True,
                            None: False, }
        iProfileOpen = CHECKBOX_MAPPING.get(request.POST.get("iProfileOpen"))
        iForbidSE = CHECKBOX_MAPPING.get(request.POST.get("iForbidSE"))
        iGetNews = CHECKBOX_MAPPING.get(request.POST.get("iGetNews"))

        # getting selects
        time_zone = YgTimezone.objects.get(title=request.POST.get("iTimeZone"))
        notif_freq_value = NOTIF_FREQ[request.POST.get("iNotifFreq")]


        # saving config to db
        user_config, created = YgUserConfig.objects.get_or_create(user=request.user)

        user_config.profile_open = iProfileOpen
        user_config.se_forbidden = iForbidSE
        user_config.get_news = iGetNews

        user_config.time_zone = time_zone

        user_config.save()

        # saving notification frequency to db
        user_notif_freq, created = YgUserNotification.objects.get_or_create(user=request.user)
        user_notif_freq.notification_freq = notif_freq_value
        user_notif_freq.save()

        # getting info from db
        user_data = get_config_data(request.user)
        vocabularies = get_config_vocabularies()

        # setting save message
        messages = {}
        messages["bottom"] = '<div class="config__message config__message_green">Настройки успешно сохранены</div>'
        return render(request, "ygProfile/user-config.html", {"user_data": user_data, "messages": messages,
                                                              "vocabularies": vocabularies, "menu_links": MENU_LINKS})
