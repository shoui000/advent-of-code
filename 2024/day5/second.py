f = open('data.txt', 'r')

map = {}
invalids = []
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

        if not valid:
            invalids.append(nms)

f.close()

def sort(array):
    while True:
        valid = True
        for i, n in enumerate(array):

            for x, y in map.items():

                if n in y and x in array:
                    if array.index(x) > i:
                        valid = False
                        array[array.index(x)], array[array.index(n)] = n, x
                        continue
        if valid:
            break

    return array

sum = 0
for nms in invalids:
    nms = sort(nms)
    sum += int(nms[len(nms) // 2])

print(sum)
