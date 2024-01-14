import { readFile } from "node:fs";

const rankOfCards = ['A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2']

readFile('./data.txt', (err, input) => {
    if (err) throw err;

    const hands = parseData(input);
    
    const sortedHands = bubble_sort(hands);

    let firstPuzzle = 0;
    sortedHands.forEach((hand, index) => {firstPuzzle += (hand.bid * (index + 1))})

    console.log('First Puzzle:', firstPuzzle);
});

function bubble_sort(list1){
    for (let i = 0; i < list1.length-1; i++){
        for (let j = 0; j < list1.length-1; j++){
            if(compare(list1[j], list1[j+1])){
                let temp = list1[j]
                list1[j] = list1[j+1]
                list1[j+1] = temp
            }
        }
    }

    return list1;

    function compare(hand1, hand2){
        if (hand1.combination > hand2.combination){
            return true;
        } else if (hand1.combination == hand2.combination){
            for (let i = 0; i <= 5; i++){
                if (hand1.cards[i] != hand2.cards[i]){
                    if (rankOfCards.indexOf(hand1.cards[i]) < rankOfCards.indexOf(hand2.cards[i])){
                        return true;
                    } else {
                        return false;
                    }
                }
            }
        } else {
            return false;
        }
    }
}

function findCombination(hand){
    if (hand.cards.match(/(.)\1{4}/)){
        // five of a kind
        hand.combination = 7;
    } else {
        const table = readCards(hand.cards);
        
        if (table.includes(4)){
            // four of a kind;
            hand.combination = 6;
        } else if (table.includes(3) && table.includes(2)){
            // fullhouse
            hand.combination = 5;
        } else if (table.includes(3) && !table.includes(2)){
            // three of a kind;
            hand.combination = 4;
        } else if (table.includes(2) && (table.indexOf(2) != table.lastIndexOf(2))){
            // two pairs;
            hand.combination = 3;
        } else if (table.includes(2) && !(table.indexOf(2) != table.lastIndexOf(2))){
            // a pair;
            hand.combination = 2;
        } else {
            hand.combination = 1;
        }

        function readCards(cards){
            let table = [];
            let read = [];

            for (const card of cards){
                if (!read.includes(card)){
                    let occur = 0;

                    for (const cardC of cards){
                        if (cardC == card) occur += 1; 
                    }

                    table.push(occur);
                    read.push(card)
                }
            }

            return table;
        };
    }
    return hand;
}

function parseData(input){
    const info = input.toString('utf8').split('\n').slice(0,-1);

    let hands = info.map((line, index) => {
        const data = line.split(' ');

        return {id: index, cards: data[0], bid: Number(data[1])}
    });

    hands = hands.map(findCombination);

    return hands
};
