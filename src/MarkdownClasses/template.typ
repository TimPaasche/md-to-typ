//set page properties
#set page(
   paper: "a4",
   margin: (top: 3cm, bottom: 2cm, left: 1.5cm, right: 1.5cm)
 )
 
//set text properties
#set text(font: "Roboto", size: 12pt, lang: "en") 

//set heading properties
#set heading(numbering: "1.1.1." )

//set paragraph properties
#set par(
justify: true,
linebreaks: "optimized"
)  

// set outline proerties
#set outline(
indent: auto,
)

// set figure properties
#set figure.caption(separator: [.])

// Define Code-Block Style
#show raw.where(block: true): local-raw => {
    align(center, block(
      fill: luma(240),
      inset: 10pt,
      radius: 4pt,
      width: 90%,
      align(left, local-raw))
    )
}

#set table(
  stroke: none,
  gutter: 0.2em,
  fill: (x, y) =>
    if x == 0 { gray } else { silver },
  inset: (right: 1.5em),
)

