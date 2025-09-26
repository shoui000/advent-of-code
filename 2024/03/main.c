#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>

#define BUFFER_SIZE 1024

void firstPuzzle(FILE *f) {
  int a, b, sum = 0, res;
  char t;

  while (1) {
    res = fscanf(f, "mul(%d,%d%c", &a, &b, &t);

    if (res == EOF) {
      break;
    }

    if (res == 0) {
      fgetc(f);
      continue;
    }

    if (res == 3 && t == ')') {
      if (a < 0 || a > 999 || b < 0 || b > 999) {
        continue;
      }
      sum += a * b;
    }
  }

  printf("FirstPuzzle: the response is %d\n", sum);
}

int main(int argc, char *argv[]) {
  if (argc < 2) {
    printf("Main: the option was not passed\n");
    exit(EXIT_FAILURE);
  }

  FILE *f;

  switch (atoi(argv[1])) {
    case 0:
      f = fopen("./input_test.txt", "r");
      break;
    case 1:
      f = fopen("./input.txt", "r");
      break;
    default:
      printf("Main: Wrong option\n");
      exit(EXIT_FAILURE);
  }

  if (f == NULL) {
    printf("Main: Error while reading input file\n");
    exit(EXIT_FAILURE);
  }

  firstPuzzle(f);

  fclose(f);

  exit(EXIT_SUCCESS);
}
