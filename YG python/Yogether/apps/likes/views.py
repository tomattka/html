from django.shortcuts import render
from django.views.generic.base import View
from .models import YgLikes
from userProfile.models import YgUser
from userConfig.functions import send_notification
from search.functions import search_result_html
from django.urls import reverse


MENU_LINKS = {"likes": "profile-menu__item_selected"}


class LikeSet(View):
    def get(self, request):
        try:
            user_from = request.GET["user_from"]
            user_to = request.GET["user_to"]
            mode = request.GET["mode"]
            if mode == 'like':
                YgLikes.objects.get_or_create(user_from_id=user_from, user_to_id=user_to)
                send_notification(user_to, "Вы получили симпатию от пользователя на сайте.")
            else:
                row = YgLikes.objects.get(user_from_id=user_from, user_to_id=user_to)
                row.delete()
            result = 'success'
        except:
            result = 'error'
        return render(request, 'ygProfile/result-only.html', {'result': result})


def get_single_user_html(user_id):
    user = YgUser.objects.filter(id=user_id)  # filter not get, cause need iterable for next function
    result = search_result_html(user)
    return result


def get_likes_users(user_id, like_type):
    id_list = []

    if like_type == 'incoming' or like_type == 'matched':
        likes = YgLikes.objects.filter(user_to_id=user_id).order_by("-id")
        id_list = likes.values_list('user_from_id', flat=True)
        # Mark as viewed
        likes = likes.filter(is_viewed=False)
        for like in likes:
            like.is_viewed = True
            like.save()

    if like_type == 'outcoming':
        likes = YgLikes.objects.filter(user_from_id=user_id).order_by("-id")
        id_list = likes.values_list('user_to_id', flat=True)

    if like_type == 'matched':
        likes = YgLikes.objects.filter(user_from_id=user_id, user_to_id__in=id_list).order_by("-id")  # id_list already got list who liked user
        id_list = likes.values_list('user_to_id', flat=True)

    result = ''
    for user_id in id_list:
        result += get_single_user_html(user_id)
    result = result.replace("search__item", "likes__item")
    return result


class LikesIncoming(View):
    def get(self, request):
        users_html = get_likes_users(request.user.id, 'incoming')
        header_html = f'<h3>Симпатии пользователей</h3> <a href="{reverse("likes_matched")}">Взаимные симпатии</a> <a href="{reverse("likes_outcoming")}">Мои симпатии</a>'
        return render(request, "ygLikes/likes.html", {"menu_links": MENU_LINKS, "users": users_html, "header_html": header_html})


class LikesOutcoming(View):
    def get(self, request):
        users_html = get_likes_users(request.user.id, 'outcoming')
        header_html = f'<h3>Мои симпатии</h3> <a href="{reverse("likes_matched")}">Взаимные симпатии</a> <a href="{reverse("likes_incoming")}">Симпатии пользователей</a>'
        return render(request, "ygLikes/likes.html", {"menu_links": MENU_LINKS, "users": users_html, "header_html": header_html})


class LikesMatched(View):
    def get(self, request):
        users_html = get_likes_users(request.user.id, 'matched')
        header_html = f'<h3>Взаимные симпатии</h3> <a href="{reverse("likes_incoming")}">Симпатии пользователей</a> <a href="{reverse("likes_outcoming")}">Мои симпатии</a>'
        return render(request, "ygLikes/likes.html", {"menu_links": MENU_LINKS, "users": users_html, "header_html": header_html})
