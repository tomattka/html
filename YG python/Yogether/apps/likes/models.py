from django.db import models
from userProfile.models import YgUser


class YgLikes(models.Model):
    user_from = models.ForeignKey(YgUser, related_name='user_like_from', verbose_name='Сам пользователь', blank=False, null=False,
                                  on_delete=models.CASCADE)
    user_to = models.ForeignKey(YgUser, related_name='user_like_to', verbose_name='Кого залайкал', blank=False, null=False,
                                    on_delete=models.CASCADE)
    is_viewed = models.BooleanField(verbose_name='Просмотрено', default=False)
