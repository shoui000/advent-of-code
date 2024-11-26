f = open('data.txt', 'r')

sum = 0

for l in f.readlines():
    l = l.strip()
    z, x = l.split(':')

    gameId = int(z.split(' ', 1)[1])
    x = x.strip().split(';')

    gameMax = {
            'red': 0,
            'green': 0,
            'blue': 0
            }

    for hand in x:
        hand = hand.split(',')

        for cube in hand:
            cube = cube.strip()
            amnt, color = cube.split(' ')

            amnt = int(amnt)

            if gameMax[color] == 0 or gameMax[color] < amnt:
                gameMax[color] = amnt

    if gameMax['red'] <= 12 and gameMax['green'] <= 13 and gameMax['blue'] <= 14:
        sum += gameId

print(sum)

f.close()
