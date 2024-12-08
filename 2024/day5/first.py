f = open('data.txt', 'r')

map = {}
sum = 0
rules = True
for l in f.readlines():
    l = l.strip()
    
    if l == "":
        rules = False
        continue

    if rules:
        g, a = l.split("|")

        if g not in map:
            map[g] = []

        map[g].append(a)

    if not rules:
        nms = l.split(',')
        valid = True

        for i, n in enumerate(nms):

            for x, y in map.items():

                if n in y and x in nms:
                    if nms.index(x) > i:
                        valid = False
                        continue

        if valid:
            sum += int(nms[len(nms) // 2])

f.close()

print(sum)

