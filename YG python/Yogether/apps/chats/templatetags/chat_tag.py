from django import template
from chats.models import YgMessage

register = template.Library()


@register.simple_tag()
def get_unread_messages(user_id):
    amount = YgMessage.objects.filter(user_to_id=user_id, is_read=False).count()
    result = f' <span class="amount-circle">{amount}</span>' if amount > 0 else ''
    return result

