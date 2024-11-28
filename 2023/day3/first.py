f = open("data.txt", "r")

numbers = []
symbols = []

for i, l in enumerate(f.readlines()):
    l = l.strip()

    previousNumber = False
    for j, c in enumerate(l):

        if c.isdigit():
            if not previousNumber:
                numbers.append({
                    'num' : c,
                    'pos': [[i,j]]
                    })
                previousNumber = True
            elif previousNumber:
                numbers[-1]['num'] = numbers[-1]['num'] + c
                numbers[-1]['pos'].append([i,j])
                previousNumber = True
        elif c != '.':
            symbols.append([i,j])
            previousNumber = False 
        else:
            previousNumber = False

f.close()

sum = 0
for l, c in symbols:
    valid = [[l-1, c-1],[l-1, c],[l-1, c+1],
            [l, c-1],           [l, c+1],
            [l+1, c-1],[l+1, c],[l+1, c+1]]

# muito feio isso aqui, ta validando todos os números a cada simbolo, tem que melhorar, coisa horrível
    for n in numbers:
        if any(v in n['pos'] for v in valid):
            sum += int(n['num'])

print(sum)

