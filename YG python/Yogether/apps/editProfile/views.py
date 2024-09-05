from django.shortcuts import render
from django.views.generic.base import View
from userProfile.functions import getUserData, getVocabularies, get_tags_sql
from .functions import getUserInfo, get_location, get_birth_date, get_gender, get_marital_status, get_experience, \
    set_user_doctrines, set_user_tradition, set_user_practices, set_user_interests
import json


class EditProfile(View):
    def get(self, request):
        user_info = getUserData(request.user.id, request.user.gender)
        vocabularies = getVocabularies(request.user.gender)
        menu_links = {"my_page": "profile-menu__item_selected"}
        return render(request, "ygProfile/profile-edit.html", {"user_info": user_info, "vocabularies": vocabularies,
                                                               "menu_links": menu_links})


class GetHints(View):
    def getHints(self, value, YgModel):
        items = YgModel.objects.filter(title__icontains=value).order_by('title')[:5]
        result = '';
        for item in items:
            result += str(item) + "<br>"
        return result

    # Vocabularies: location (list), doctrines (tags), traditions (tags), practices (tags), interests (tags)
    def get(self, request):
        value = request.GET["value"]
        vocabulary = request.GET["vocabulary"]

        if vocabulary == "location":
            from userProfile.models import YgLocation
            result = self.getHints(value, YgLocation)

        if vocabulary == 'interests':
            from userProfile.models import YgInterest
            result = self.getHints(value, YgInterest)

        if vocabulary == 'doctrines':
            from userProfile.models import YgDoctrine
            result = self.getHints(value, YgDoctrine)

        if vocabulary == 'traditions':
            from userProfile.models import YgTradition
            result = self.getHints(value, YgTradition)

        if vocabulary == 'practices':
            from userProfile.models import YgPractice
            result = self.getHints(value, YgPractice)

        return render(request, "ygProfile/result-only.html", {"result": result})


class SaveParameter(View):
    def get(self, request):
        value = request.GET["value"]
        parameter = request.GET["parameter"]

        result = "success"
        user_info = getUserInfo(request.user.id)

        # Saving to database
        if parameter == "location":
            user_info.location = get_location(value)

        if parameter == "gender":
            request.user.gender = get_gender(value)
            request.user.save()

        if parameter == "marital":
            user_info.marital_status = get_marital_status(value)

        if parameter == "experience":
            user_info.experience = get_experience(value)

        if parameter == "interests":
            set_user_interests(user_info, value)

        if parameter == "doctrines":
            set_user_doctrines(user_info, value)

        if parameter == "traditions":
            set_user_tradition(user_info, value)

        if parameter == "practices":
            set_user_practices(user_info, value)

        if parameter == "about":
            user_info.about = value

        # request
        if parameter == "request":
            user_info.request = value

        # name
        if parameter == "name":
            family = request.GET["family"]
            if value:
                request.user.first_name = value
                request.user.last_name = family
                request.user.save()

        # --------------------- params with castom messages -------------------
        # birthday
        if parameter == "birthday":
            user_info.birth_date = get_birth_date(value)
            result = user_info.getAge

        # ---------------------- common data save -----------------------------
        user_info.save()

        return render(request, "ygProfile/result-only.html", {"result": result})


class SaveAll(View):
    def post(self, request):
        json_data = json.loads(request.body)

        user = request.user
        user_info = getUserInfo(request.user.id)

        user.first_name = json_data["first_name"]
        user.last_name = json_data["last_name"]
        user.gender = get_gender(json_data["gender"])
        user.save()

        user_info.location = get_location(json_data["location"])
        user_info.birth_date = get_birth_date(json_data["birth_date"])
        user_info.marital_status = get_marital_status(json_data["marital_status"])
        user_info.experience = get_experience(json_data["experience"])
        user_info.about = json_data["about"]
        user_info.request = json_data["request"]

        set_user_doctrines(user_info, json_data["doctrine"])
        set_user_tradition(user_info, json_data["tradition"])
        set_user_practices(user_info, json_data["practice"])
        set_user_interests(user_info, json_data["interests"])

        user_info.save()

        return render(request, "ygProfile/result-only.html", {"result": "success"})

    def get(self, request):
        return render(request, "ygProfile/result-only.html",
                      {"result": "This file is for saving user data from mobile"})


class UserInterests(View):
    def get(self, request):
        cursor = get_tags_sql("practices", request.user.id)
        result = ""
        for row in cursor:
            result += str(row[1]) + ' '

        return render(request, "ygProfile/result-only.html", {"result": result})
