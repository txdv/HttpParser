DIR=HttpParser
HTTP=$(DIR)/http_method.cs
ERRNO=$(DIR)/http_errno.cs

all: generate $(HTTP) $(ERRNO)

generate: generate.c
	gcc generate.c -o generate

$(HTTP): generate
	./generate http > $(HTTP)

$(ERRNO): generate
	./generate errno > $(ERRNO)

clean:
	rm -f generate $(HTTP) $(ERRNO)
