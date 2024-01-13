import { readFile } from "node:fs";

readFile('./data.txt', (err, input) => {
    if (err) throw err;

    const content = parseData(input);
    
    const info = findPossibleWays(content);

    // first solution:
    let firstSolution = 1;
    info.forEach(run => {
        firstSolution *= run.ways
    });

    console.log('First Solution:', firstSolution);
    
    const scpContent = scpParseData(input);

    const scpInfo = findPossibleWays(scpContent);

    console.log('Second Solution:', scpInfo[0].ways);
});

function scpParseData(input){
    const info = input.toString('utf8').split('\n').slice(0,-1);
    
    const lines = info.map(line => {
        const title = line.split(':')[0];
        const data = line.split(':')[1].match(/\d+/g).map(x => Number(x));

        return [title, data];
    });

    const runs = [{time: '', distance: ''}];

    lines[0][1].forEach(number => runs[0].time += number);
    lines[1][1].forEach(number => runs[0].distance += number);
    
    runs[0].time = Number(runs[0].time);
    runs[0].distance = Number(runs[0].distance);
    
    return runs;
};

function findPossibleWays(content){
    return content.map(run => {
        run.ways = 0;
        for (let i  = 0; i <= run.time; i++){
            let distanceTravelled = i * (run.time - i);

            if (distanceTravelled > run.distance){
                run.ways++
            }
        }

        return run;
    })
};

function parseData(input){
    const info = input.toString('utf8').split('\n').slice(0,-1);
    
    const lines = info.map(line => {
        const title = line.split(':')[0];
        const data = line.split(':')[1].match(/\d+/g).map(x => Number(x));

        return [title, data];
    });

    const runs = [];

    lines[0][1].forEach((time, index) => {
        let distance = lines[1][1][index];
        let run = {time: time, distance: distance}

        runs.push(run)
    })

    return runs;
};
