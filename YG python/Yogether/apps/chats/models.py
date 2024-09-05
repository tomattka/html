from django.db import models
from userProfile.models import YgUser


class YgMessage(models.Model):
    user_from = models.ForeignKey(YgUser, related_name='user_from', verbose_name='От кого', blank=False, null=True, on_delete=models.CASCADE)
    user_to = models.ForeignKey(YgUser, related_name='user_to', verbose_name='Кому', blank=False, null=True, on_delete=models.CASCADE)
    time = models.DateTimeField(verbose_name='Когда')
    text = models.TextField(verbose_name='Текст', max_length=2000)
    is_read = models.BooleanField(verbose_name='Прочитано', default=False)
