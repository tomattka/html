from django import template
from likes.models import YgLikes

register = template.Library()


@register.simple_tag()
def get_new_likes(user_id):
    amount = YgLikes.objects.filter(user_to_id=user_id, is_viewed=False).count()
    result = f' <span class="amount-circle">{amount}</span>' if amount > 0 else ''
    return result