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

void secondPuzzle(FILE *f) {
  int sum = 0, index = 0, state = 1, res, a, b;
  char buffer[BUFFER_SIZE], t, x;

  char doing[] = { 'd', 'o', '(', ')' };
  char notdoing[] = { 'd', 'o', 'n', '\'', 't', '(', ')' };

  t = fgetc(f);
  while (1) {
    if (feof(f)) break;
    
    if (t != doing[index] && t != notdoing[index] && t != 'm') {
      index = 0;
    } else if (t == doing[index]) {
      if (index == 3) {
        state = 1; index = 0;
      } else {
        index++;
      }
    } else if (t == notdoing[index]) {
      if (index == 6) {
        state = 0; index = 0;
      } else {
        index++;
      }
    } else if (state && t == 'm') {
      res = fscanf(f, "ul(%d,%d%c", &a, &b, &x);

      if (res == EOF) break;

      if (res == 3) {
        if (x != ')') {
          t = x;
          continue;
        }

        if (!(a < 0 || a > 999 || b < 0 || b > 999) && state) {
          sum += a * b;
        }
      }
    }

    t = fgetc(f);
  }

  printf("SecondPuzzle: the response is %d\n", sum);

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

  secondPuzzle(f);

  fclose(f);

  exit(EXIT_SUCCESS);
}
