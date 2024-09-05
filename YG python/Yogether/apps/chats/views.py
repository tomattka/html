from django.shortcuts import render, redirect
from django.templatetags.static import static
from django.views.generic.base import View
from django.urls import reverse
from django.utils import timezone
from django.db import connection
from .models import YgMessage
from .functions import get_messages, format_time
from userConfig.functions import send_notification
from userProfile.models import YgUser
from userProfile.functions import getUser



menu_links = {"messages": "profile-menu__item_selected"}


# Create your views here.
class PrivateChat(View):
    def get(self, request, user_id, mode='full'):
        if user_id:
            chat_user = getUser(user_id)
            chat_user.profile_url = reverse('user_profile', kwargs={'user_id': user_id})
            messages = get_messages(chat_user, request)

            render_parameters = {"chat_user": chat_user, "messages": messages, "menu_links": menu_links}
            if mode == 'full':
                return render(request, "ygChats/private-chat.html", render_parameters)
            else:
                return render(request, "ygChats/private-chat-plain.html", render_parameters)
        else:
            return redirect('chat_list')


class ChatList(View):
    def get(self, request):
        # Getting sql result
        cursor = connection.cursor()
        user_id = request.user.id
        # menu_unread = menu_unread_messages(user_id)
        query = f'''select id, time, text, user_from_id, user_to_id from chats_ygmessage where id in
                        (select max(id) as message_id from
                            (select user_from_id as user_id, max(id) as id from chats_ygmessage
                            where user_to_id={user_id}
                            group by user_from_id
                            union
                            select user_to_id as user_id, max(id) as id from chats_ygmessage
                            where user_from_id={user_id}
                            group by user_to_id) q_all_msg
                        group by user_id)
                    order by time desc'''
        cursor.execute(query)

        # Html results
        result = ''
        if cursor:

            try:
                time_offset = YgUserConfig.objects.get(user=request.user).time_zone.offset
            except:
                time_offset = 3

            for item in cursor:

                # "Me" label
                me_label = ''
                user_with_id = item[3]
                if user_with_id == request.user.id:
                    user_with_id = item[4]
                    me_label = '<b>Я:</b> '

                # Getting user data
                user_with = YgUser.objects.get(id=user_with_id)
                if user_with.profile_pic:
                    userpic = user_with.profile_pic.url
                else:
                    userpic = static('img/user-profile/no-photo.png')

                # Getting unread messages count
                unread_count = YgMessage.objects.filter(user_from_id=user_with, user_to_id=request.user.id, is_read=False).count()
                unread_html = f'<div class="amount-circle">{unread_count}</div>' if unread_count > 0 else ''

                # Forming html
                result += f'''<div id="chat_{user_with_id}" class="chat-list__item">
                                <div class="chat-list__pic"><img src="{userpic}" alt="{user_with.first_name} {user_with.last_name}"></div>
                                <div class="chat-list__content">                            
                                    <div class="chat-list__name">{user_with.first_name} {user_with.last_name}</div>                            
                                    <div class="chat-list__time">{format_time(item[1], time_offset)}</div>                            
                                    <div class="chat-list__text">{me_label}{item[2]}</div>
                                    {unread_html}
                                </div>
                            </div>'''
        return render(request, "ygChats/chat-list.html", {"message_list": result, "menu_links": menu_links})


class SendMessage(View):
    def get(self, request):
        my_id = request.GET["my_id"]
        with_id = request.GET["with_id"]
        msg = request.GET["msg"]
        time = timezone.now()

        yg_message = YgMessage()
        yg_message.user_from_id = my_id
        yg_message.user_to_id = with_id
        yg_message.time = time
        yg_message.text = msg
        yg_message.save()

        result = 'from ' + my_id + " to " + with_id + ": " + msg + '<br>time: ' + str(time)
        result += '<br>SAVED'

        send_notification(with_id)
        return render(request, "ygProfile/result-only.html", {"result": result})


class CheckMessages(View):
    def get(self, request):
        with_id = request.GET["with_id"]
        last_msg = int(request.GET["last_msg"])

        chat_user = getUser(with_id)
        messages = get_messages(chat_user, request, last_msg)
        return render(request, "ygProfile/result-only.html", {"result": messages})


class PreviousMessages(View):
    def get(self, request):
        with_id = request.GET["with_id"]
        first_msg = int(request.GET["first_msg"])

        chat_user = getUser(with_id)
        messages = get_messages(chat_user, request, first_msg=first_msg)
        return render(request, "ygProfile/result-only.html", {"result": messages})
