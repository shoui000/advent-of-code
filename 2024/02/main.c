#include <string.h>
#include <stdio.h>
#include <stdlib.h>

#define BUFFER_SIZE 100
#define LEVEL_BUFFER_SIZE 5

typedef struct {
  size_t size;
  int *numbers;
} level;

void firstPuzzle(level *a, int n){
  int direction = 0, difference = 0, safe = 1, sum = 0;
  for (int i = 0; i < n; i++) {
    safe = 1;
    direction = ((a[i].numbers[0] - a[i].numbers[1]) < 0);

    for (int j = 0; j < a[i].size-1; j++) {
      difference = a[i].numbers[j] - a[i].numbers[j+1];

      if (abs(difference) == 0 || abs(difference) > 3 || !(difference < 0 == direction)) {
        safe = 0;
        break;
      }
    }

    if (safe) {
      sum++;
    }
  }

  printf("FirstPuzzle: The response is %d\n", sum);
}

int isSafe(level a) {
  int direction = 0, difference = 0, safe = 1;
  direction = ((a.numbers[0] - a.numbers[1]) < 0);

  for (size_t j = 0; j < a.size-1; j++) {
    difference = a.numbers[j] - a.numbers[j+1];

    if (abs(difference) == 0 || abs(difference) > 3 || !(difference < 0 == direction)) {
      safe = 0;
      break;
    }
  }

  return safe;
}

void secondPuzzle(level *a, int n){
  int sum = 0;

  for (size_t i = 0; i < n; i++) {
    if (!isSafe(a[i])) {
      level temp;

      for (size_t j = 0; j < a[i].size; j++) {
        temp.size = a[i].size-1;
        temp.numbers = malloc(temp.size * sizeof(int));

        memcpy(temp.numbers, a[i].numbers, j * sizeof(int));
        memcpy(temp.numbers+j, a[i].numbers+j+1, (temp.size-j) * sizeof(int));

        if (isSafe(temp)) {
          sum++;
          free(temp.numbers);
          break;
        }

        free(temp.numbers);
      }

    } else { sum++; }
  }

  printf("SecondPuzzle: The response is %d\n", sum);
}

int main(int argc, char *argv[])
{
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

  size_t capacity = BUFFER_SIZE;
  int n = 0;
  char buffer[BUFFER_SIZE];

  level *a = malloc(capacity * sizeof(level));
  if (a == NULL) {
    printf("Main: Memory allocation failed\n");
    exit(EXIT_FAILURE);
  }


  while (1) {
    if (n >= capacity) {
      capacity += BUFFER_SIZE;
      a = realloc(a, capacity * sizeof(level));

      if (a == NULL) {
        printf("Main: Memory allocation failed\n");
        exit(EXIT_FAILURE);
      }
    }

    if (fgets(buffer, BUFFER_SIZE, f) != NULL) {
      int i = 0, level_capacity = LEVEL_BUFFER_SIZE;
      char *b;

      a[n].numbers = malloc(level_capacity * sizeof(int));

      if (a == NULL) {
        printf("Main: Memory allocation failed\n");
        exit(EXIT_FAILURE);
      }

      b = strtok(buffer, " ");
      while (b != NULL){
        if (i >= level_capacity) {
          level_capacity += LEVEL_BUFFER_SIZE;
          a[n].numbers = realloc(a[n].numbers, level_capacity * sizeof(int)); 

          if (a == NULL) {
            printf("Main: Memory allocation failed\n");
            exit(EXIT_FAILURE);
          }
        }

        a[n].numbers[i] = atoi(b);
        i++;
        b = strtok(NULL, " ");
      }

      a[n].size = i;

    } else {
      break;
    }

    n++;
  }

  firstPuzzle(a, n);
  secondPuzzle(a, n);

  exit(EXIT_SUCCESS);
}
