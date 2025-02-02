# Generated by Django 3.2.3 on 2022-09-02 21:44

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
            name='YgLikes',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('is_viewed', models.BooleanField(default=False, verbose_name='Просмотрено')),
                ('user_from', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, related_name='user_like_from', to=settings.AUTH_USER_MODEL, verbose_name='Сам пользователь')),
                ('user_to', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, related_name='user_like_to', to=settings.AUTH_USER_MODEL, verbose_name='Кого залайкал')),
            ],
        ),
    ]
