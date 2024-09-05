from django.urls import path
from django.contrib.auth.decorators import login_required
from . import views

urlpatterns = [
    path('set-like.html', login_required(views.LikeSet.as_view()), name='like_user'),
    path('users.html', login_required(views.LikesIncoming.as_view()), name='likes_incoming'),
    path('mine.html', login_required(views.LikesOutcoming.as_view()), name='likes_outcoming'),
    path('matched.html', login_required(views.LikesMatched.as_view()), name='likes_matched'),
]