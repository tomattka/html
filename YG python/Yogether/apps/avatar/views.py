from django.conf import settings
from django.shortcuts import render
from django.views.generic.base import View
from django.core.files.storage import default_storage
from django.shortcuts import redirect
from PIL import Image
import os
from userProfile.models import YgUser
from photos.functions import CropPILonly, get_random_str


class AvatarLoad(View):
    def get(self, request):
        return render(request, "ygPhotos/avatar-load.html")

    def post(self, request):

        try:
            iFile = request.FILES["iFile"]
        except:
            iFile = None

        # ------------ Saving file to crop --------------
        if iFile:
            # setting id + random str name
            try:
                ext = iFile.name.split(".")[-1]
            except:
                ext = 'jpg'
            file_name = str(request.user.id) + '_' + get_random_str() + '.' + ext

            file_name = default_storage.save("user/avatar_temp/" + file_name, iFile) # !!!!!!!!!! here file name
            file_url = default_storage.url(file_name)
            try:
                img = Image.open(os.path.normpath(settings.MEDIA_ROOT + "/" + file_name))
            except:
                os.remove(os.path.normpath(settings.MEDIA_ROOT + "/" + file_name))
                print("Error processing file with PIL")
                return redirect('profile_edit')
            return render(request, "ygPhotos/avatar-preview.html", {"file_url": file_url, "file_name": file_name})

        # ------------ Showing file to crop --------------
        else:
            # crop photo
            file_name = request.POST["iFileName"]
            iDelete = request.POST["iDelete"]
            if iDelete == "false":
                iX = int(request.POST["iX"])
                iY = int(request.POST["iY"])
                iW = int(request.POST["iW"])

                img = Image.open(os.path.normpath(settings.MEDIA_ROOT + "/" + file_name))
                img_cropped = img.crop((iX, iY, iX + iW, iY + iW))

                file_name = img.filename.replace("\\", "/").split('/')[-1] # replace is local bug fix

                path = "user/profile_pic/" + file_name

                if img_cropped.mode in ("RGBA", "P"):
                    img_cropped = img_cropped.convert("RGB")
                img_cropped.save(os.path.normpath(settings.MEDIA_ROOT + "/" + path))

                user_id = request.user.id
                user_yg = YgUser.objects.filter(id=user_id)[0]

                # avatar db save
                user_yg.profile_pic = path

                # full photo
                img_full = CropPILonly(img, 1920, 1280, False)
                path_full = "user/profile_pic/full/" + file_name

                if img_full.mode in ("RGBA", "P"):
                    img_full = img_full.convert("RGB")
                img_full.save(os.path.normpath(settings.MEDIA_ROOT + "/" + path_full))
                user_yg.profile_pic_full = path_full

                user_yg.save()

                os.remove(img.filename)

            else:  # if delete flag is on
                os.remove(os.path.normpath(settings.MEDIA_ROOT + "/" + file_name))

            return redirect('profile_edit')


class AvatarDelete(View):
    def get(self, request):
        request.user.profile_pic = request.user.profile_pic_full = None
        request.user.save()
        return redirect('profile_edit')

