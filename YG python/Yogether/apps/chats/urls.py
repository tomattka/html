from django.urls import path
from django.contrib.auth.decorators import login_required
from . import views


urlpatterns = [
    path('', login_required(views.ChatList.as_view()), name='chat_list'),
    path('<int:user_id>/', login_required(views.PrivateChat.as_view()), name='chat_private'),
    path('<str:mode>/<int:user_id>/', login_required(views.PrivateChat.as_view()), name='chat_private_simple'),
    path('send-message.html', login_required(views.SendMessage.as_view()), name='chat_send_message'),
    path('check-messages.html', login_required(views.CheckMessages.as_view()), name='chat_check_messages'),
    path('previous-messages.html', login_required(views.PreviousMessages.as_view()), name='chat_previous_messages'),
]