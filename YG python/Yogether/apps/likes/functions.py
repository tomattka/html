from .models import YgLikes


def get_user_liked(user_from, user_to):
    liked = True if len(YgLikes.objects.filter(user_from_id=user_from, user_to_id=user_to)) > 0 else False
    if liked:
        result = {"value": "true", "css": "user-view__like_active", "css_mobile": "user-info__like_active"}
    else:
        result = {"value": "false", "css": "", "css_mobile": ""}
    return result
