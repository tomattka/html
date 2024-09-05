from django.urls import path
from . import views

urlpatterns = [
    path('', views.UserSearch.as_view(), name='user_search'),
    path('get-results.html', views.GetResults.as_view(), name='search_results'),
]
