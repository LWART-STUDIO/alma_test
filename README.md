# Тестовое задание
## Общее описание задачи:
В рамках туристического маркетинга необходимо разработать интерактивный
картографический каталог.
На карте (любой, на ваш выбор) пользователи должны иметь возможность
добавлять информационные пины о различных местах. Эти информационные пины
могут содержать информацию о городах, важных местах, природных заповедниках,
флоре или фауне. Все пины должны содержать название, одно изображение и текст, а
также звуковое описание (необязательно), которое будет отображаться на панели.
Пины сохраняются и после перезапуска ПО.
## Пользователь должен иметь возможность:
- Добавить новый пин, нажав на карту.
- Сохранить все пины.
- Предпросмотра пина во всплывающем окне и подробного просмотра пина по нажатию
кнопки «Читать дальше».
- Переместить пин после длительного нажатия на нее.
- Редактировать информацию о пине.
- Удалить пин.
## Используемые технологии:
- Unity 2021 LTS или более поздняя версия
- Оптимизация загрузки/сохранения обязательна (время загрузки будет измерено)
- Использование DoTween будет плюсом
# Принятые решения
- Использованы синглтоны вместо DI, мало объектов, работают быстрее чем Zenject
- Использование json для сохранения данных. Картинки сохраняеются в кеш, и в виде пути к файлу.
Можно сделать дублирование картинки туда же куда и основное сохранение и грузить потом оттуда.
Сама загрузка сделана с помощью [Native File Picker](https://assetstore.unity.com/packages/tools/integration/native-file-picker-for-android-ios-173238?srsltid=AfmBOooqQ0PkVru-lTLBbPPGPfFQz0sU7MalDbbCIKcWZwJnWaj3DH2j) и [Unity-ImageLoader](https://github.com/IvanMurzak/Unity-ImageLoader).
- Для анимации использовался [DoTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)
- Для UI были использованы [True Shadow](https://assetstore.unity.com/packages/tools/gui/true-shadow-ui-soft-shadow-and-glow-205220) и [Translucent Image](https://assetstore.unity.com/packages/tools/gui/translucent-image-fast-ui-background-blur-78464) в роли украшающих эллементов
- Карта взята скрином из Google Maps
  

https://github.com/user-attachments/assets/09c8f51e-4a4c-4a1b-9427-107ae9301ce4


