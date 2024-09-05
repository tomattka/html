from datetime import datetime, timedelta
from django.db.models import Q
from django.templatetags.static import static
from django.utils import timezone
from .models import YgMessage
from userConfig.models import YgUserConfig


def format_time(msg_time, time_offset=3):
    msg_time += timedelta(hours=time_offset)
    msg_date = msg_time.date()
    today_date = (timezone.now() + timedelta(hours=time_offset)).date()
    if msg_date == today_date:
        result = 'сегодня'
    else:
        if msg_date == today_date - timedelta(days=1):
            result = 'вчера'
        else:
            result = msg_time.strftime('%d.%m')
            if msg_time.year < datetime.today().year:
                result += msg_time.strftime('.%y')
    result += ' в ' +  msg_time.strftime('%H:%M')

    return result


def get_message_html(msg, user_with, request, time_offset=3):
    if msg.user_from.id == request.user.id:
        msg_html = f'<div id="msg_{msg.id}" class="chat__message message message_to">'
        msg_html += '<div class="message__body">'
        msg_html += '<div class="message__header">'
        msg_html += '<div class="message__name">Я</div>'
        msg_html += f'<div class="message__time">{format_time(msg.time, time_offset)}</div>'
        msg_html += '</div>'
        msg_html += f'<div class="message__text">{msg.text}</div>'
        msg_html += '</div>'
        msg_html += f'<div class="message__corner"><img src="{static("img/chat/corner_right.gif")}"></div>'
        msg_html += '</div><div class="clear"></div>'
    else:
        msg_html = f'<div id="msg_{msg.id}" class="chat__message message message_from">'
        msg_html += f'<div class="message__corner"><img src="{static("img/chat/corner_left.gif")}"></div>'
        msg_html += '<div class="message__body">'
        msg_html += '<div class="message__header">'
        msg_html += f'<div class="message__name">{user_with.first_name} {user_with.last_name} </div>'
        msg_html += f'<div class="message__time">{format_time(msg.time, time_offset)}</div>'
        msg_html += '</div>'
        msg_html += f'<div class="message__text">{msg.text}</div>'
        msg_html += '</div>'
        msg_html += '</div><div class="clear"></div>'
    return msg_html


def get_messages(user_with, request, last_msg=0, first_msg=0, max_amount=20): # user object, not id
    if request.user != user_with:
        messages = YgMessage.objects.filter(Q(user_from=user_with) | Q(user_to=user_with))\
            .filter(Q(user_from=request.user) | Q(user_to=request.user))
    else:
        messages = YgMessage.objects.filter(user_from=user_with, user_to=user_with)

    if last_msg > 0:
        messages = messages.filter(id__gt=last_msg).order_by('time')
    else:
        if first_msg > 0:
            messages = messages.filter(id__lt=first_msg)

        messages = messages.order_by('-time')[:max_amount:-1]

    try:
        time_offset = YgUserConfig.objects.get(user=request.user).time_zone.offset
    except:
        time_offset = 3

    messages_html = ''
    for msg in messages:
        messages_html += get_message_html(msg, user_with, request, time_offset)
        if (msg.user_from != request.user) or (msg.user_from == msg.user_to):
            msg.is_read = True
            msg.save()
    return messages_html
