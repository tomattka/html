from django.conf import settings
from django.core.files.storage import default_storage
from django.templatetags.static import static
from PIL import Image
import os
import random, string
from .models import YgUserPhoto


# crop PIL image and return it
def CropPILonly(imagePIL, width, height=0, is_square=True):
    result = imagePIL

    original_width, original_height = result.size

    # square crop
    if is_square:
        height = width

        if original_width > original_height:
            # horizontal photo
            new_height = height
            new_width = round(original_width * new_height / original_height)
            result = result.resize((new_width, new_height))
            crop_x = round((new_width - width) / 2)
            result = result.crop((crop_x, 0, crop_x + width, height))
        else:
            # vertical photo
            new_width = width
            new_height = round(original_height * new_width / original_width)
            result = result.resize((new_width, new_height))
            crop_y = round((new_height - height) / 2)
            result = result.crop((0, crop_y, width, crop_y + height))

    # proportional resize
    else:
        if (original_width > width) or (original_height > height):
            if (original_width / original_height) > (width / height):
                # resize by width
                new_width = width
                new_height = round(original_height * width / original_width)
            else:
                # resize by height
                new_height = height
                new_width = round(original_width * height / original_height)
            result = result.resize((new_width, new_height))

    return result


def get_random_str(size=6):
    chars = string.ascii_lowercase + string.digits
    return ''.join(random.choice(chars) for _ in range(size))


# save file and crop it, returning file name or None if error
def CropPhoto(file_name, photo, folder, width, height=0, is_square=True):
    # folder example: "user/photos_temp/"
    path = default_storage.save(folder + file_name, photo)
    try:
        result = Image.open(os.path.normpath(settings.MEDIA_ROOT + "/" + path))
    except:
        print("ERROR: File is not an image!")
        os.remove(os.path.normpath(settings.MEDIA_ROOT + "/" + path))
        result = False

    if result:
        result = CropPILonly(result, width, height, is_square)

        if result.mode in ("RGBA", "P"):
            result = result.convert("RGB")

        result.save(settings.MEDIA_ROOT + "/" + path)
        return path

    else:
        return None


def save_file(file, request):
    user_photo = YgUserPhoto(user=request.user)
    user_id = request.user.id

    try:
        ext = file.name.split(".")[-1]
    except:
        ext = 'jpg'
    file_name = str(user_id) + '_' + get_random_str() + '.' + ext

    user_photo.photo_big = CropPhoto(file_name, file, "user/photos/big/", 1920, 1280, False)

    if (user_photo.photo_big):  # it's not empty if there was an image
        user_photo.photo_medium = CropPhoto(file_name, file, "user/photos/medium/", 200)
        user_photo.photo_small = CropPhoto(file_name, file, "user/photos/small/", 60)
        user_photo.save()  # saving new item

        user_photo.order = user_photo.id
        user_photo.save()  # saving order


def add_files(request):
    try:
        iFiles = request.FILES.getlist("iFiles")
    except:
        iFiles = None
    if iFiles:
        for file in iFiles:
            save_file(file, request)


def htmlUserPhotos(request):
    # getting photos
    photos = ''
    user_photos = YgUserPhoto.objects.filter(user=request.user).order_by('-order', '-id')

    for photo in user_photos:

        photoId = str(photo.id)
        description = str(photo.description).replace("None", "")

        photos += f'<div id="item{photoId}"  class="photos__item">'
        photos += f'<div class="photos__photo"><a href="{photo.photo_big.url}" data-fancybox="gallery"><img src="{photo.photo_big.url}" height="200" /></a></div>'
        photos += '<div class="photos__description">'
        photos += f'<textarea id="desc{photoId}" name="desc{photoId}" class="input-simple" placeholder="Описание">{description}</textarea>'
        photos += '</div>'
        photos += '<div class="photos__arrows">'
        photos += '<img src="' + static('img/photos-edit/arrow_up.svg') + '" width="30" class="arrow-up" alt="Вверх">'
        photos += '<img src="' + static('img/photos-edit/arrow_down.svg') + '" width="30" class="arrow-down" alt="Вниз">'
        photos += '</div>'
        photos += '<div class="photos__delete">'
        photos += '<img src="' + static('img/photos-edit/delete.svg') + '" width="30" alt="Удалить">'
        photos += '</div>'
        photos += '<div class="photos__loading">'
        photos += '<img src="' + static('img/common/loading.gif') + '">'
        photos += '</div>'
        photos += '</div>\n'

    return photos
