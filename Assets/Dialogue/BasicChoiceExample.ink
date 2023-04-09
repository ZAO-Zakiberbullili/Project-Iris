->Talking
=== Talking ===
    Bla-Bla-Blas
    -> MainChoices

 
=== MainChoices ===
    + [I`nm soo tactic #tactic]
        Say smth tactic
        ->EXIT
    + [I`m so sarcastic #sarcastic]
        Say smth sarcastic
        ->EXIT
    + [I`m so direct #direct]
        Sat smth direct
        -> EXIT
    + [Another input #another]
        -> anotherChoices
    + [Questions #asking]
        -> questions
    
    
=== anotherChoices ===
    + [Insight #insight]
        Say insight
        -> anotherChoices
    + [Sattellite #sattellite]
        say sattelite
        -> anotherChoices
    + [Flirt #flirt]
        go flirt
        -> anotherChoices
    + [Back #back]
        -> MainChoices
    
=== questions ===
    * [Question ONE #question]
        first question
        -> questions
    * [Question TWO #question]
        second question
        -> questions
    * [Question THREE #question]
        third question
        -> questions
    + [Back #back]
        -> MainChoices


=== EXIT ===
   ->END
