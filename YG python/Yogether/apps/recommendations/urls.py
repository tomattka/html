from django.urls import path
from . import views

urlpatterns = [
    path('remove.html', views.RecommendRemove.as_view(), name='recommend_remove'),
    path('cancel-remove.html', views.RecommendCancelRemove.as_view(), name='recommend_cancel_remove'),
    path('reset.html', views.RecommendReset.as_view(), name='recommend_reset'),
]