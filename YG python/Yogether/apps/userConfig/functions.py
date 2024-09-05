from datetime import timedelta
from django.utils import timezone
from django.core.mail import EmailMultiAlternatives
from django.template.loader import render_to_string
from django.utils.html import strip_tags
from userProfile.models import YgUser
from .models import YgUserNotification, YgUserConfig


def html_email(title, text):
    html = f'''
        <html lang="ru">
        <head>
            <meta http-equiv="content-type" content="text/html; charset=utf-8" />
            <title>{title}</title>
        </head>
        <body>
            <table cellpadding="0" cellspacing="0" border="0" style="margin: 0; padding: 0; min-width: 320px; max-width: 700px; width: 100%;">
                <tr>
                    <td style="background-color: #F3E3FF; padding: 10px;"><a href="http://yogether.ru" target="_blank"><img src="http://yogether.ru/img/mail/logo.png" width="180" alt="Yogether" /></a></td>
                </tr>
                <tr>
                    <td style="font-family: Raleway, Tahoma, Geneva, Verdana, sans-serif; height: 240px; vertical-align: top; padding: 20px 15px 45px 15px; font-size: 16px;">
                        <h1 style="color: #49497D; text-align: center; font-size: 32px;">{title}</h1>
                        {text}
                    </td>
                </tr>
                <tr>
                    <td style="font-family: Raleway, Tahoma, Geneva, Verdana, sans-serif; background-color: #F3E3FF; padding: 10px; text-align: center; color: #49497D; font-size: 14px;">
                        <a href="http://yogether.ru/page/about" style="color: #49497D; margin: 0px 5px;">О проекте</a> | <a href="http://yogether.ru/search/" style="color: #49497D; margin: 0px 5px;">Участники</a> | <a href="http://yogether.ru/page/contacts" style="color: #49497D; margin: 0px 5px;">Контакты</a>
                    </td>
                </tr>
            </table>
        </body>
        </html>
    '''
    return html


def send_notification(user_to_id, notif_text='Вы получили сообщение на сайте.'):
    # Время по времени серверу (вроде бы)
    notif_text = f'<p style="text-align: center;">{notif_text}</p>'

    # Получить пользователя и мейл
    yg_user = YgUser.objects.get(id=user_to_id)
    mail_to = yg_user.email

    # Получить параметры уведомлений
    notification, created = YgUserNotification.objects.get_or_create(user=yg_user)
    notif_freq = notification.notification_freq
    notif_last = notification.last_notif_time

    time_enough = True
    if notif_last and (timezone.now() - timedelta(hours=notif_freq) < notif_last):
        time_enough = False

    notif_title = 'Уведомление сайта Yogether'
    if notif_freq != 0 and (notif_freq == 999 or time_enough):

        # sending message
        html_body = html_email(notif_title, notif_text)
        message = EmailMultiAlternatives(
            subject=notif_title,
            body=html_body,
            from_email='Yogether <noreply@yogether.ru>',    # вынести в конфиг
            to=[mail_to]
        )
        message.attach_alternative(html_body, "text/html")
        message.send(fail_silently=False)

        # Записать в базу время уведомления
        notification.last_notif_time = timezone.now()
        notification.save()

        print(f"Уведомление отправлено на {mail_to}")

    else:
        print(f"Отправка запрещена: {notif_freq}, {notif_last}")


# def activate_timezone(user): # maybe unused
#     try:
#         user_timezone = YgUserConfig.objects.get(user=user).time_zone.value
#     except:
#         user_timezone = 'Europe/Moscow'
#
#     timezone.activate(user_timezone)
