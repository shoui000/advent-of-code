import { readFile } from "node:fs";

console.time('teste');
readFile('./data.txt', (err, input) => {
    if (err) throw err;

    const info = parseData(input);
    const allSeeds = info.seeds.map(seedNumber => makeSeed(seedNumber, info.tables));

    const newSeedNumbers = getNewSeedNumbers(info.seeds);

    let leastLocation = allSeeds[0].location;
    allSeeds.forEach(seed => {
        if (seed.location < leastLocation){
            leastLocation = seed.location
        }
    });

    console.log(`First Puzzle:`, leastLocation);

    const actualLeastLocation = findLeastLocationByRangeSeedNumbers([newSeedNumbers[0]], info.tables);
    console.log(`Second Puzzle:`, actualLeastLocation);
    console.timeEnd('teste')
});

function findLeastLocationByRangeSeedNumbers(seedRanges, tables){
    let leastLocation = makeSeed(seedRanges[0].start, tables).location;
    seedRanges.forEach(pair => {
        for (let i = pair.start; i <= pair.end; i++){
            let seed = makeSeed(i, tables);
            leastLocation = Math.min(leastLocation, seed.location);
        }
    })
    return leastLocation;
};

function getNewSeedNumbers(seedNumbers){
    let newSeedNumbers = [];
    seedNumbers.forEach((number, index) => {
        if (index % 2 == 0){
            let pair = {start: number, length: seedNumbers[index+1], end: number+seedNumbers[index+1]};
            newSeedNumbers.push(pair)
        }
    });
    return newSeedNumbers;
};

function makeSeed(seedNumber, tables){
    let seed = {seed: seedNumber};

    let data = ['soil', 'fertilizer', 'water', 'light', 'temperature', 'humidity', 'location'];

    data.forEach((information, index) => {
        let previousInformation = data[index-1] ?? 'seed'
        let table = tables[`${previousInformation}-to-${information}`]

        seed[information] = convertSourceToDestination(seed[previousInformation], table);
    });

    return seed;
};

function parseData(data){
    let info = data.toString('utf8').split('\n\n').map(x=>x.split('\n').filter(x=>x!=''));

    let seeds = info[0][0].slice(7).split(' ').map(x => Number(x));

    let information = {seeds: seeds, tables: []};

    info.slice(1).forEach(table => {
        let tableName = table[0].slice(0, table[0].indexOf(' '));
        let tableContent = table.slice(1).map(x => x.split(' ').map(x => Number(x)));
        information.tables[tableName] = tableContent;
    });

    return information;
};

function convertSourceToDestination(input, mapLines){
    let returnn;

    mapLines.every(map => {
        let destinationRangeStart = map[0];
        let sourceRangeStart = map[1];
        let rangeLength = map[2];

        if (input < sourceRangeStart || input >= sourceRangeStart+rangeLength){
            return true;
        } else {
            let inputIndex = input-sourceRangeStart;
            if (inputIndex >= 0){
                returnn = destinationRangeStart+inputIndex;
                return false;
            }
        }
        return true;
    });

    if (returnn == undefined){
        returnn = input
    };

    return returnn;
};
