# Generated by Django 3.2.3 on 2022-09-02 21:45

from django.conf import settings
from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    initial = True

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
    ]

    operations = [
        migrations.CreateModel(
            name='YgUserPhoto',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('photo_big', models.ImageField(blank=True, null=True, upload_to='user/photos/big', verbose_name='Большое фото')),
                ('photo_medium', models.ImageField(blank=True, null=True, upload_to='user/photos/medium', verbose_name='Среднее превью')),
                ('photo_small', models.ImageField(blank=True, null=True, upload_to='user/photos/small', verbose_name='Малое превью')),
                ('description', models.CharField(blank=True, max_length=2000, null=True, verbose_name='Описание')),
                ('order', models.IntegerField(default=0, verbose_name='Порядок')),
                ('moderated', models.BooleanField(default=False, verbose_name='Проверено')),
                ('user', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL, verbose_name='Пользователь')),
            ],
        ),
    ]
