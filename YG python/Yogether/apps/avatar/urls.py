from django.urls import path
from . import views
from django.contrib.auth.decorators import login_required

urlpatterns = [
    path('load.html', login_required(views.AvatarLoad.as_view()), name='avatar_load'),
    path('delete.html', login_required(views.AvatarDelete.as_view()), name='avatar_delete'),
]
