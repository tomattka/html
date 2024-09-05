from django.db import models

class YgPage(models.Model):
    title = models.CharField(max_length=500, verbose_name='Заголовок', blank=True, null=True)
    desc = models.CharField(max_length=2000, verbose_name='Описание', blank=True, null=True)
    text = models.TextField(verbose_name='Текст', blank=True, null=True)
    alias = models.CharField(max_length=500, verbose_name='Псевдоним', blank=True, null=True)
    seoTitle = models.CharField(max_length=500, verbose_name='SEO Title', blank=True, null=True)
    seoKeys = models.CharField(max_length=1000, verbose_name='SEO Keys', blank=True, null=True)
    seoDesc = models.CharField(max_length=1000, verbose_name='SEO Desc', blank=True, null=True)
    published = models.BooleanField(verbose_name='Опубликовано', default=False, blank=True, null=True)
    date_created = models.DateField(verbose_name='Опубликовано', blank=True, null=True)
    last_edited = models.DateField(verbose_name='Изменено', blank=True, null=True)
