REGISTRY=localhost:5000
FRONTEND_IMAGE=reactapp
BACKEND_IMAGE=todoapi

.PHONY: all registry build push deploy

init:
	docker	run	-d	-p	5000:5000	--name	registry	registry:2
	docker	build	-t	$(BACKEND_IMAGE):latest	-f	ToDocker/Dockerfile .
	docker	build	-t	$(FRONTEND_IMAGE):latest	-f	clientapp/Dockerfile	./clientapp
	docker	tag	$(BACKEND_IMAGE)	$(REGISTRY)/$(BACKEND_IMAGE)
	docker	tag	$(FRONTEND_IMAGE)	$(REGISTRY)/$(FRONTEND_IMAGE)
	docker	push	$(REGISTRY)/$(BACKEND_IMAGE)
	docker	push	$(REGISTRY)/$(FRONTEND_IMAGE)
	docker-compose	up	-d

back:
	docker	build	-t	$(BACKEND_IMAGE)	-f	ToDocker/Dockerfile .
	docker	push	$(REGISTRY)/$(BACKEND_IMAGE):latest
	docker-compose	down
	docker-compose	up	-d

front:
	docker	build	-t	$(FRONTEND_IMAGE)	-f	clientapp/Dockerfile	./clientapp
	docker	push	$(REGISTRY)/$(FRONTEND_IMAGE):latest
	docker-compose	down
	docker-compose	up	-d

	
