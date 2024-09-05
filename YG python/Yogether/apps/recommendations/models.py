from django.db import models
from userProfile.models import YgUser


class YgRecommendRemove(models.Model):
    user_self = models.ForeignKey(YgUser, related_name='user_self', verbose_name='Сам пользователь', blank=False, null=False,
                                  on_delete=models.CASCADE)
    user_remove = models.ForeignKey(YgUser, related_name='user_remove', verbose_name='Удалить из рекомендаций пользователя', blank=False, null=False,
                                    on_delete=models.CASCADE)
