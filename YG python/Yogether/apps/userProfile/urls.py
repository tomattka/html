from django.urls import path
from . import views

urlpatterns = [
    # path('my-page/', views.UserProfile.as_view(), name='user_profile'),
    path('logincheck.html', views.LoginCheck.as_view()),
    path('<int:user_id>/', views.UserProfile.as_view(), name='user_profile'),
    path('', views.UserRedirect.as_view(), name='user_redirect')
]