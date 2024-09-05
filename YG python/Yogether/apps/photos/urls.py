from django.urls import path
from . import views
from django.contrib.auth.decorators import login_required

urlpatterns = [
    path('', login_required(views.PhotosEdit.as_view()), name='photos_edit'),
    path('deletePhoto.html', login_required(views.PhotoDelete.as_view()), name='photo_delete'),
    path('savePhotos.html', login_required(views.PhotosSave.as_view()), name='photos_save'),
    path('orderPhotos.html', login_required(views.PhotosOrder.as_view()), name='photos_order'),
    path('addPhotos.html', login_required(views.PhotosAdd.as_view()), name='photos_add'),
]
