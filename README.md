# ToDocker
Для развёртывания необходимы Docker и Makefile, команда

`make deploy`

### Пошагово

В Makefile первым делом задаются названия образов `REGISTRY=localhost:5000` `FRONTEND_IMAGE=reactapp` `BACKEND_IMAGE=todoapi`, затем запускается процедура `deploy`


Запуск локального регистра на порте 5000

`docker	run	-d	-p	5000:5000	--name	registry	registry:2`

Билд приложений в образы, тегирование

`docker	build	-t	$(BACKEND_IMAGE):latest	-f	ToDocker/Dockerfile .`\
`docker	build	-t	$(FRONTEND_IMAGE):latest	-f	clientapp/Dockerfile	./clientapp`\
`docker	tag	$(BACKEND_IMAGE)	$(REGISTRY)/$(BACKEND_IMAGE)`\
`docker	tag	$(FRONTEND_IMAGE)	$(REGISTRY)/$(FRONTEND_IMAGE)`

Отправка образов в регистр

`docker	push	$(REGISTRY)/$(BACKEND_IMAGE)`\
`docker	push	$(REGISTRY)/$(FRONTEND_IMAGE)`

Деплой

`docker-compose up -d`

Процедуры `back` и `front` фиксируют изменения в коде, создают новые изображения, отправляют их в реестр, и пересобирают приложение, отдельно для фронта и для бэка.

Образы лежат в файлах контейнера registry, по пути `var/cache/registry/v2/repositories` (вообще, репозиторий может быить и удалённым)

В docker-compose в качестве образов используются образы из регистра, (например, образ бэкенда `todoapi:` 
`image: localhost:5000/todoapi`), в остальном файл стандартный.