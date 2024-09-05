from django.db import models
from userProfile.models import YgUser

class YgUserPhoto(models.Model):
    user = models.ForeignKey(YgUser, verbose_name='Пользователь', on_delete=models.CASCADE, blank=True, null=True)
    photo_big = models.ImageField(upload_to='user/photos/big', verbose_name='Большое фото', blank=True, null=True)
    photo_medium = models.ImageField(upload_to='user/photos/medium', verbose_name='Среднее превью', blank=True, null=True)
    photo_small = models.ImageField(upload_to='user/photos/small', verbose_name='Малое превью', blank=True, null=True)
    description = models.CharField(max_length=2000, verbose_name='Описание', blank=True, null=True)
    order = models.IntegerField(default=0, verbose_name='Порядок')
    moderated = models.BooleanField(verbose_name='Проверено', default=False)
