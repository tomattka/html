from django.urls import path
from django.contrib.auth.decorators import login_required
from . import views

urlpatterns = [
    path('edit.html', login_required(views.EditProfile.as_view()), name='profile_edit'),
    path('get-hints.html', login_required(views.GetHints.as_view()), name='get_hints'),
    path('save-parameter.html', login_required(views.SaveParameter.as_view()), name='save_parameter'),
    path('save-all.html', login_required(views.SaveAll.as_view()), name='save_all'),
    path('temp.html', login_required(views.UserInterests.as_view()), name='temp'),
]