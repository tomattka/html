# See short filter logic descriprion below code
from django.shortcuts import render
from django.views.generic.base import View
from userProfile.models import YgUser, YgUserInfo
from userProfile.functions import getVocabularies, get_noindex_tags
from userProfile.forms import YGLoginForm
from recommendations.functions import get_recommendations
from .functions import search_result_html, do_search
from django.db.models import Q
import json


USERS_DISPLAY = 24


class GetResults(View):
    def post(self, request):
        json_data = json.loads(request.body)
        result = do_search(json_data["request"], json_data["params"])
        # recommend = get_recommendations(request.user.id, request.user.id) if request.user.is_authenticated else ''
        return render(request, "ygProfile/result-only.html", {"result": result, "form": YGLoginForm})


class UserSearch(View):
    def get(self, request):
        vocabularies = getVocabularies()
        users = YgUser.objects.filter(~Q(yguserconfig__profile_open=False)).order_by('-id')[:USERS_DISPLAY]
        result = search_result_html(users)
        # recommend = get_recommendations(request.user.id, request.user.id) if request.user.is_authenticated else ''
        return render(request, "ygSearch/user-search.html", {"result": result, "vocabularies": vocabularies,
                                                             "form": YGLoginForm})


# ЛОГИКА фильтов:
# Все параметры передаются аджаксом в виде текстовых значений в вид GetResults
# Там вначале фильтруется по параметрам в функции get_users_by_params, а потом среди них текстовый поиск
# Там по очереди применяются по необходимости соответствующие параметрам фильтры
