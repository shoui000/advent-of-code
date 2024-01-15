import { readFile } from "node:fs";

readFile('./data.txt', (err, input) => {
    if (err) throw err;

    const data = parseData(input);

    const firstPuzzle = readInstructions(data);
    const secondPuzzle = readInstructionsForGhosts(data);

    console.log('First Puzzle:', firstPuzzle);
    console.log('Second Puzzle:', secondPuzzle);
})

function parseData(input) {
    const content = input.toString('utf8').split('\n').slice(0, -1);
    const commands = content[0];
    const maps = content.slice(2);

    const parsedMaps = {};

    maps.forEach(line => {
        let title = line.slice(0, 3);

        let left = line.slice(7, 10);
        let right = line.slice(12, -1);

        parsedMaps[title] = {L: left, R: right};
    })

    return {instructions: commands, maps: parsedMaps};
};

function readInstructionsForGhosts(data){
    const starters = Object.entries(data.maps)
        .filter(obj => obj[0][2] == 'A')
        .map(obj => obj[1]);

    const counters = starters.map(pos => {
        let counter = 0;

        while (true){
            let next;

            for (const char of data.instructions){
                counter += 1;
                next = pos[char];

                if (next[2] == 'Z') {
                    break
                } else {
                    pos = data.maps[next]
                };
                
            }

            if (next[2] == 'Z') break;
        }

        return counter;
    })

    function mdc(a, b){
        if (a == 0){
            return b
        } else if (b == 0){
            return a
        } else {
            let r = Math.max(a, b) % Math.min(a, b);
            
            return mdc(Math.min(a, b), r);
        };
    };

    function mmc(a, b){
        return (a * b) / mdc(a,b);
    }

    let mdccc;
    counters.forEach((count, index) => {
        if (index == 0){
            mdccc = mmc(count, counters[index+1])
        } else {
            mdccc = mmc(count, mdccc);
        }
    })

    return mdccc;
};

function readInstructions(data){
    let counter = 0;
    let pos = data.maps['AAA'];

    while (pos != data.maps['ZZZ']){
        for (const char of data.instructions){
            counter += 1;
            pos = data.maps[pos[char]];
            if (pos == data.maps['ZZZ']) break;
        }
    }

    return counter;
};
