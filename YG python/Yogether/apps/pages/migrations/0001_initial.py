# Generated by Django 3.2.3 on 2022-09-02 21:45

from django.db import migrations, models


class Migration(migrations.Migration):

    initial = True

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='YgPage',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('title', models.CharField(blank=True, max_length=500, null=True, verbose_name='Заголовок')),
                ('desc', models.CharField(blank=True, max_length=2000, null=True, verbose_name='Описание')),
                ('text', models.TextField(blank=True, null=True, verbose_name='Текст')),
                ('alias', models.CharField(blank=True, max_length=500, null=True, verbose_name='Псевдоним')),
                ('seoTitle', models.CharField(blank=True, max_length=500, null=True, verbose_name='SEO Title')),
                ('seoKeys', models.CharField(blank=True, max_length=1000, null=True, verbose_name='SEO Keys')),
                ('seoDesc', models.CharField(blank=True, max_length=1000, null=True, verbose_name='SEO Desc')),
                ('published', models.BooleanField(blank=True, default=False, null=True, verbose_name='Опубликовано')),
                ('date_created', models.DateField(blank=True, null=True, verbose_name='Опубликовано')),
                ('last_edited', models.DateField(blank=True, null=True, verbose_name='Изменено')),
            ],
        ),
    ]
