from django.urls import path
from . import views

urlpatterns = [
    path('<str:alias>/', views.Page.as_view(), name='page'),
]
