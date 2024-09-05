from django.contrib.auth.models import AbstractUser
from django.db import models
from datetime import date


class YgGender(models.Model):
    title = models.CharField(max_length=100, verbose_name='Заголовок')
    order = models.IntegerField(default=0, verbose_name='Порядок')

    def __str__(self):
        return self.title

    class Meta:
        verbose_name = verbose_name_plural = "Гендер"


class YgLocation(models.Model):
    title = models.CharField(max_length=200, verbose_name='Заголовок')
    moderated = models.BooleanField(verbose_name='Проверено', default=False)

    def __str__(self):
        return self.title

    class Meta:
        verbose_name = verbose_name_plural = "Местоположение"


class YgMaritalStatus(models.Model):
    title = models.CharField(max_length=100, verbose_name='Заголовок')
    title_fem = models.CharField(max_length=100, verbose_name='Написание для женщин', blank=True, null=True)
    title_neutral = models.CharField(max_length=200, verbose_name='Нейтральное написание', blank=True, null=True)
    order = models.IntegerField(default=0, verbose_name='Порядок')

    def __str__(self):
        return self.title

    class Meta:
        verbose_name = verbose_name_plural = "Семейное положение"


class YgExperience(models.Model):
    title = models.CharField(max_length=100, verbose_name='Заголовок')
    order = models.IntegerField(default=0, verbose_name='Порядок')

    def __str__(self):
        return self.title

    class Meta:
        verbose_name = verbose_name_plural = "Опыт практики"


class YgDoctrine(models.Model):
    title = models.CharField(max_length=200, verbose_name='Заголовок')
    short_desc = models.CharField(max_length=1000, verbose_name='Краткое описание', blank=True, null=True)
    full_info = models.TextField(verbose_name='Полная информация', blank=True, null=True)
    moderated = models.BooleanField(verbose_name='Проверено', default=False)

    def __str__(self):
        return self.title

    class Meta:
        verbose_name = "Учение"
        verbose_name_plural = "Учения"


class YgTradition(models.Model):
    title = models.CharField(max_length=200, verbose_name='Название')
    short_desc = models.CharField(max_length=1000, verbose_name='Краткое описание', blank=True, null=True)
    full_info = models.TextField(verbose_name='Полная информация', blank=True, null=True)
    moderated = models.BooleanField(verbose_name='Проверено', default=False)

    def __str__(self):
        return self.title

    class Meta:
        verbose_name = "Традиция"
        verbose_name_plural = "Традиции"


class YgPractice(models.Model):
    title = models.CharField(max_length=200, verbose_name='Название')
    short_desc = models.CharField(max_length=1000, verbose_name='Краткое описание', blank=True, null=True)
    full_info = models.TextField(verbose_name='Полная информация', blank=True, null=True)
    moderated = models.BooleanField(verbose_name='Проверено', default=False)

    def __str__(self):
        return self.title

    class Meta:
        verbose_name = "Практика"
        verbose_name_plural = "Практики"


class YgInterest(models.Model):
    title = models.CharField(max_length=200, verbose_name='Заголовок')
    moderated = models.BooleanField(verbose_name='Проверено', default=False)

    def __str__(self):
        return self.title

    class Meta:
        verbose_name = "Интерес"
        verbose_name_plural = "Интересы"


class YgUser(AbstractUser):
    """ Custom user model """
    _profile_pic = models.ImageField(upload_to='user/profile_pic', verbose_name='Аватар', blank=True, null=True)
    profile_pic_full = models.ImageField(upload_to='user/profile_pic/full', verbose_name='Аватар полный', blank=True, null=True)
    gender = models.ForeignKey(YgGender, on_delete=models.SET_NULL, verbose_name='Гендер', blank=True, null=True)
    is_online = models.BooleanField(verbose_name='Онлайн', default=False, null=True)
    social_data_loaded = models.BooleanField(verbose_name='Загружены данные соцсети', default=False, null=True)

    """ Temp fix serialization problem """
    @property
    def profile_pic(self):
        return self._profile_pic

    @profile_pic.setter
    def profile_pic(self, value):
        self._profile_pic = value

    def __str__(self):
        return self.username + ' (' + self.first_name + ' ' + self.last_name + ')'

    class Meta:
        verbose_name = "Пользователь"
        verbose_name_plural = "Пользователи"


class YgUserInfo(models.Model):
    user = models.OneToOneField(YgUser, on_delete=models.CASCADE, primary_key=True, verbose_name='Пользователь')
    birth_date = models.DateField(verbose_name='Дата рождения', blank=True, null=True)
    about = models.TextField(verbose_name='О себе', blank=True, null=True)
    request = models.CharField(max_length=2000, verbose_name='Ищу', blank=True, null=True)
    location = models.ForeignKey(YgLocation, verbose_name='Место жительства', on_delete=models.SET_NULL, blank=True, null=True)
    marital_status = models.ForeignKey(YgMaritalStatus, verbose_name='Семейное положение', on_delete=models.SET_NULL, blank=True, null=True)
    experience = models.ForeignKey(YgExperience, verbose_name='Опыт практики', on_delete=models.SET_NULL, blank=True, null=True)
    doctrines = models.ManyToManyField(YgDoctrine, verbose_name='Учения', blank=True)
    traditions = models.ManyToManyField(YgTradition, verbose_name='Традиции', blank=True)
    practices = models.ManyToManyField(YgPractice, verbose_name='Практики', blank=True)
    interests = models.ManyToManyField(YgInterest, verbose_name='Интересы', blank=True)

    def __str__(self):
        return str(self.user)

    def getAge(self):
        res = ''
        if self.birth_date:
            today = date.today()
            born = self.birth_date
            years = today.year - born.year - ((today.month, today.day) < (born.month, born.day)) # True to int is 1
            age_label = 'лет'
            if years < 5 or years > 20:
                if years % 10 == 1:
                    age_label = 'год'
                if years % 10 in (2, 3, 4):
                    age_label = 'года'
            res = str(years) + ' ' + age_label
        return res

    def getBirthDateForField(self):
        res = None
        if self.birth_date:
            res = self.birth_date.strftime('%Y-%m-%d')
        return res

    class Meta:
        verbose_name = "Данные пользователя"
        verbose_name_plural = "Данные пользователей"


# class YgUserInterests(models.Model):
#     user_info = models.ForeignKey(YgUserInfo, on_delete=models.CASCADE)
#     interest = models.ForeignKey(YgInterest, on_delete=models.CASCADE)



