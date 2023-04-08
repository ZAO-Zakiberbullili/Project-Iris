->Talking
=== Talking ===
    Bla-Bla-Blas
    -> MainChoices

 
=== MainChoices ===
    *** I`m soo tactic #tactic
        Say smth tactic
        ->END
    *** I`m so sarcastic #sarcastic
        Say smth sarcastic
        ->END
    *** I`m so direct #direct
        Sat smth direct
        -> END
    *** Another input #another
        -> anotherChoices
    *** Questions #asking
        -> questions
    
    
=== anotherChoices ===
    *** Insight #insight
        Say insight
        -> anotherChoices
    *** Sattellite #sattellite
        say sattelite
        -> anotherChoices
    *** Flirt #flirt
        go flirt
        -> anotherChoices
    *** Back #back
        -> MainChoices
    
=== questions ===
    *** Question ONE #question
        first question
        -> questions
    *** Question TWO #question
        second question
        -> questions
    *** Question THREE #question
        third question
        -> questions
    *** Back #back
        -> MainChoices
    

