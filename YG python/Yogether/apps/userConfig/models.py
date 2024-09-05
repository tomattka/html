from django.db import models
from userProfile.models import YgUser


class YgTimezone(models.Model):  # словарь
    title = models.CharField(max_length=1000, verbose_name='Описание таймзоны')
    value = models.CharField(max_length=100, verbose_name='Техническое значение')
    offset = models.IntegerField(verbose_name='Разница с UTC', default=0)
    order = models.IntegerField(verbose_name='Порядок', default=0)

    def __str__(self):
        return str(self.title)

    class Meta:
        verbose_name = "Часовой пояс"
        verbose_name_plural = "Часовые пояса"
        ordering = ('order',)


class YgUserConfig(models.Model):
    user = models.OneToOneField(YgUser, verbose_name='Пользователь', blank=False, null=False,
                                on_delete=models.CASCADE)
    profile_open = models.BooleanField(verbose_name='Показывать в поиске и рекомендациях', default=True)
    se_forbidden = models.BooleanField(verbose_name='Запретить индексацию', default=False)
    time_zone = models.ForeignKey(YgTimezone, on_delete=models.SET_NULL, verbose_name='Часовая зона', blank=True,
                                  null=True, default=1)
    get_news = models.BooleanField(verbose_name='Получать новости', default=True)


#-------------------------------------------------
class YgUserNotification(models.Model):
    user = models.OneToOneField(YgUser, verbose_name='Пользователь', blank=False, null=False,
                                on_delete=models.CASCADE)
    notification_freq = models.IntegerField(verbose_name='Частота уведомлений', default=1)  # hours,0=never,999=always
    last_notif_time = models.DateTimeField(verbose_name='Время последнего уведомления', blank=True, null=True)
