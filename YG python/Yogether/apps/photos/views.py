from django.shortcuts import render
from django.views.generic.base import View
from django.shortcuts import redirect
from .models import YgUserPhoto
from .functions import add_files, htmlUserPhotos


class PhotosEdit(View):
    def get(self, request):
        menu_links = {"photos": "profile-menu__item_selected"}
        return render(request, "ygPhotos/photos-edit.html", {"photos": htmlUserPhotos(request),
                                                             "menu_links": menu_links})

    # ADDing photos (post to page itsels)
    def post(self, request):
        add_files(request)
        return redirect('photos_edit')


class PhotoDelete(View):
    def get(self, request):
        result = "success"
        photo_id = request.GET["photoId"]
        try:
            photo = YgUserPhoto.objects.get(id=photo_id, user=request.user)
            photo.delete()
        except:
            result = "no item"
        return render(request, "ygProfile/result-only.html", {"result": result})


class PhotosSave(View):
    def post(self, request):
        result = "success"
        for key in request.POST:
            if key.find("desc") > -1:
                photo_id = key.replace("desc", "")
                desc = request.POST[key]

                try:
                    photo = YgUserPhoto.objects.get(id=photo_id, user=request.user)
                    photo.description = desc
                    photo.save()
                except:
                    result = "Saving error"

        return render(request, "ygProfile/result-only.html", {"result": result})


class PhotosOrder(View):
    def get(self, request):
        result = "success"

        first_id = request.GET["first"]
        second_id = request.GET["second"]
        user_id = request.user.id

        try:
            # getting photos
            photo1 = YgUserPhoto.objects.get(id=first_id, user=request.user)
            photo2 = YgUserPhoto.objects.get(id=second_id, user=request.user)

            # changing order
            order1 = photo1.order
            photo1.order = photo2.order
            photo2.order = order1

            # saving order
            photo1.save()
            photo2.save()
        except:
            result = "No photos for such user"

        return render(request, "ygProfile/result-only.html", {"result": result})


# adding with another url (by ajax)
class PhotosAdd(View):
    def post(self, request):
        add_files(request)
        return render(request, "ygProfile/result-only.html", {"result": "success"})
