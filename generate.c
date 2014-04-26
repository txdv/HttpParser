#include "http-parser/http_parser.h"
#include <stdio.h>
#include <stdlib.h>

void print_http()
{
	printf("using System;\n\nnamespace HttpParser\n{\n\tpublic enum http_method : int\n\t{\n");
#define XX(num, name, string) printf("\t\tHTTP_%s,\n", #name, num);
	HTTP_METHOD_MAP(XX)
#undef XX
	printf("\t}\n\n");
	printf("\tpublic partial class RawHttpParser\n\t{\n\t\tprivate static string[] methodString = new string[] {\n");
#define XX(num, name, string) printf("\t\t\t\"%s\",\n", #name, num);
	HTTP_METHOD_MAP(XX)
#undef XX
	printf("\t\t};\n");
	printf("\t}\n}\n");
}

void print_errno()
{
	printf("using System;\n\nnamespace HttpParser\n{\n\tpublic enum http_errno : int\n\t{\n");
#define HTTP_ERRNO_GEN(n, s) printf("\t\tHPE_%s,\n", #n);
  HTTP_ERRNO_MAP(HTTP_ERRNO_GEN)
#undef HTTP_ERRNO_GEN
	printf("\t}\n}\n");
}

void usage()
{
		printf("Provide at least on parameter (errno, http)\n");
		exit(0);
}

int main(int argc, char **argv)
{
	if (argc < 2) {
		usage();
	}


	if (!strcmp(argv[1], "errno")) {
		print_errno();
	} else if (!strcmp(argv[1], "http")) {
		print_http();
	} else {
		usage();
	}
	return 0;
}
