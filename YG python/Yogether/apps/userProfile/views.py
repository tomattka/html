from django.shortcuts import render
from django.views.generic.base import View
from django.shortcuts import redirect
from django.urls import reverse
from .forms import YGLoginForm
from .models import YgUser
from django.db.models import Q
from django.contrib.auth import authenticate
from django.views.decorators.csrf import csrf_exempt
from .functions import getUser, getUserData, getUserPhotos, get_noindex_tags, is_enough_info
from likes.functions import get_user_liked
from recommendations.functions import get_recommendations


class UserProfile(View):
    def get(self, request, user_id):
        if user_id:
            main_info = getUser(user_id)
            user_info = getUserData(user_id, main_info.gender)
            user_photos = getUserPhotos(user_id)

            # getting if noindex
            noindex = get_noindex_tags(user_id)

            # getting recommendations
            recommend = get_recommendations(user_id, request.user.id) # user whos page is viewed

            # getting if liked
            liked = get_user_liked(request.user.id, user_id)


            if request.user.id != user_id:
                message_link = reverse('chat_private', kwargs={'user_id': user_id})
                user_info.enough_info = is_enough_info(user_info, 2)
                return render(request, "ygProfile/user-info-others.html", {"form": YGLoginForm, "main_info": main_info,
                            "user_info": user_info, "user_photos": user_photos, "message_link": message_link,
                            "noindex": noindex, "recommend": recommend, "liked": liked})
            else:
                message_link = reverse('chat_list')
                user_info.enough_info = is_enough_info(user_info, 5)
                return render(request, "ygProfile/user-info-mine.html", {"form": YGLoginForm, "user_info": user_info,
                                                                    "user_photos": user_photos,
                                                                    "message_link": message_link,
                                                                    "recommend": recommend})
        else:
            return redirect('user_search')


class UserRedirect(View):
    def get(self, request):
        if request.user.is_authenticated:
            return redirect(reverse('user_profile', kwargs={'user_id': request.user.id}))
        else:
            return redirect('user_search')



class LoginCheck(View):
    @csrf_exempt
    def get(self, request):
        username = request.GET.get('username', None)
        password = request.GET.get('password', None)
        result = 'no user'

        check_if_user_exists = YgUser.objects.filter(Q(username=username) | Q(email=username)).exists()
        if check_if_user_exists:
            user = authenticate(request, username=username, password=password)
            if user is not None:
                result = 'ok'
            else:
                result = 'wrong password'

        return render(request, 'ygProfile/result-only.html', {'result': result})
