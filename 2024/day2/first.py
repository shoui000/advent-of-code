f = open("data.txt", 'r')

sum = 0
for l in f.readlines():
    l = l.strip()
    x = l.split()

    valid = True
    direction = ''
    for i, n in enumerate(x):
        diff = int(x[i]) - int(x[i-1])

        if i != 0:

            if abs(diff) == 0 or abs(diff) > 3:
                valid = False

            elif direction == '':
                if diff < 0:
                    direction = 'down'
                elif diff > 0:
                    direction = 'up'

            elif direction != '':
                if diff < 0 and direction == 'up':
                    valid = False
                if diff > 0 and direction == 'down':
                    valid = False

    if valid:
        sum += 1

f.close()

print(sum)
