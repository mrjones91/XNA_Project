using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project03
{
    class Bot : Entity
    {
        //Map map;
        public Node location, goal;
        Node A, B, C, D;
        Node[] notes;
        ArrayList SolutionPathList;
        List<ArrayList> Steps;
        int n;
        public int tracker;
        //A variable to hold the pathnodes to know what direction

   
        public int Health
        {
            get { return health; }
        }

        

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, InputManager inputmanager)
        {
            base.LoadContent(content, inputmanager);
            moveSpeed = 60f;
            tracker = 0;
            //map = new Map();
            fileManager = new FileManager();
            moveAnimation = new SpriteSheetAnimation();
            Vector2 tempFrames = Vector2.Zero;
            SolutionPathList = new ArrayList();
            Steps = new List<ArrayList>();
            n = 0;


            B = new Node(null, null, 1, 5, 5);
            C = new Node(null, null, 1, 30, 5);
            D = new Node(null, null, 1, 35, 32);
            A = new Node(null, null, 1, 5, 35);
            notes = new Node[6];
            notes[0] = A;
            notes[1] = B;
            notes[2] = C;
            notes[3] = D;
            notes[4] = A;
            

            Waypoint(A, B);
            location = new Node();
            goal = new Node();

            fileManager.LoadContent("Load/Bot.txt", attributes, contents);
            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "Health":
                            health = int.Parse(contents[i][j]);
                            break;
                        case "Frames":
                            string[] frames = contents[i][j].Split(' ');
                            tempFrames = new Vector2(int.Parse(frames[0]), int.Parse(frames[1]));
                            break;
                        case "Image":
                            image = this.content.Load<Texture2D>(contents[i][j]);
                            break;
                        case "Position":
                            frames = contents[i][j].Split(' ');
                            position = new Vector2(int.Parse(frames[0]), int.Parse(frames[1]));
                            break;
                            

                    }

                }
            }
            moveAnimation.LoadContent(content, image, "", null, position);
            moveAnimation.Frames = tempFrames;
            moveAnimation.CurrentFrame = new Vector2(0, 0);
            location = (Node)Steps[n][tracker];
            goal = (Node)Steps[n][tracker + 1];
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            moveAnimation.UnloadContent();
        }

        public override void Update(GameTime gameTime, InputManager inputmanager, Collision col, Layers layer)
        {
            if (location != null)
                moveAnimation.IsActive = true;
           
            ////down
            //moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 0);
            //position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            ////up
            //moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 3);
            //position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            ////left
            //moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 1);
            //position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            ////right
            //moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 2);
            //position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Auto Motion Planning - make input avaliable to go anywhere from last node to next node
            if (location != null)
            {
                //if (inputmanager.KeyReleased())
                //{
                    if (goal.y < location.y)
                    {
                        moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 3);
                        position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (goal.y == location.y)
                            moveAnimation.IsActive = false;
                    }
                    else if (goal.y > location.y)
                    {
                        moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 0);
                        position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (goal.y == location.y)
                            moveAnimation.IsActive = false;
                    }

                    else if (goal.x < location.x)
                    {
                        moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 1);
                        position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (goal.x == location.x)
                            moveAnimation.IsActive = false;
                    }
                    else if (goal.x > location.x)
                    {
                        moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 2);
                        position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (goal.x == location.x)
                            moveAnimation.IsActive = false;
                    }
                    else if (goal.x == location.x && goal.y == location.y)
                    {
                        moveAnimation.CurrentFrame = new Vector2(0, 0);
                        position.X = position.X;
                        position.Y = position.Y;
                        moveAnimation.IsActive = false;
                    }
                //}
                if (moveAnimation.IsActive)
                {
                    // Update step and setup for next
                    moveAnimation.Position = position;
                    moveAnimation.Update(gameTime);
                }
               
            }

            //position updating
            {
                if (!(position.X == 160 && position.Y == 1120) && health > 0)
                {
                    tracker++;

                    if ((tracker / 32) + 1 == Steps[n].Count)
                    {
                        n++;
                        health--;
                        tracker = 0;

                        if (n < 4)
                        {
                            Waypoint(notes[n], notes[n + 1]);
                            
                        }
                        else
                        {
                            moveAnimation.IsActive = false;
                            goal = null;
                            location = null;
                        }
                            
                        Steps[n - 1] = null;

                    }
                    if (health > 0)
                    {
                        location = (Node)Steps[n][tracker / 32];
                        //if (inputmanager.KeyPressed(Keys.Left))
                        //    goal = (Node)Steps[n][(tracker / 32) - 1];
                        //else
                            goal = (Node)Steps[n][(tracker / 32) + 1];
                    }
                    else
                    {
                        location = null;
                        goal = null;
                        //throw new ArgumentOutOfRangeException("",e);
                        
                    }
                    
                }
            }

            ////Collision
            //for (int i = 0; i < col.CollisionMap.Count; i++)
            //{
            //    for (int j = 0; j < col.CollisionMap[i].Count; j++)
            //    {
            //        if (col.CollisionMap[i][j] == "b")
            //        {
            //            if (position.X + moveAnimation.FrameWidth < j * layer.TileDimensions.X ||
            //                position.X > j * layer.TileDimensions.X + layer.TileDimensions.X ||
            //                position.Y + moveAnimation.FrameHeight < i * layer.TileDimensions.Y ||
            //                position.Y > i * layer.TileDimensions.Y + layer.TileDimensions.Y)
            //            {
            //                // no collision
            //                drop = false;
            //                nodeTouch = false;
            //            }
            //            else
            //            {
            //                if (((location.x == A.x) || (location.x == B.x) || (location.x == C.x) || (location.x == D.x))
            //                    && ((location.y == A.y) || (location.y == B.y) || (location.y == C.y) || (location.y == D.y)))
            //                {
            //                    nodeTouch = true;
            //                    //health = health - 1;

            //                    if (nodeTouch && !drop)
            //                    {
            //                        drop = true;
            //                        health = health - 1;
            //                        nodeTouch = false;


            //                    }
            //                }
            //                else
            //                    drop = false;
            //            }
            //        }

                    // if (col.CollisionMap[i][j] == "n")
                    //{
                    //    if (position.X + moveAnimation.FrameWidth < j * layer.TileDimensions.X ||
                    //        position.X > j * layer.TileDimensions.X + layer.TileDimensions.X ||
                    //        position.Y + moveAnimation.FrameHeight < i * layer.TileDimensions.Y ||
                    //        position.Y > i * layer.TileDimensions.Y + layer.TileDimensions.Y)
                    //    {
                    //        // no collision
                    //        drop = false;
                    //        nodeTouch = false;
                    //    }
                    //    else
                    //    {

                    //    }

            //    }
            //}
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            moveAnimation.Draw(spriteBatch);
        }

        //static ArrayList SolutionPathList = new ArrayList();



        public void Waypoint(Node Start, Node Goal)
        {
            Node node_start = Start;
            
            SolutionPathList = new ArrayList();
            //ArrayList WayPt = new ArrayList();
            SortedCostNodeList OPEN = new SortedCostNodeList();
            SortedCostNodeList CLOSED = new SortedCostNodeList();

            OPEN.push(node_start);

            Node node_goal = Goal;

            while (OPEN.Count > 0)
            {

                Node node_current = OPEN.pop();

                if (node_current.isMatch(node_goal))
                {
                    node_goal.parentNode = node_current.parentNode;
                    break;
                }

                ArrayList successors = node_current.GetSuccessors();

                foreach (Node node_successor in successors)
                {
                    int oFound = OPEN.IndexOf(node_successor);

                    if (oFound > 0)
                    {
                        Node existing_node = OPEN.NodeAt(oFound);
                        if (existing_node.CompareTo(node_current) <= 0)
                            continue;
                    }

                    int cFound = CLOSED.IndexOf(node_successor);
                    if (cFound > 0)
                    {
                        Node existing_node = CLOSED.NodeAt(cFound);
                        if (existing_node.CompareTo(node_current) <= 0)
                            continue;
                    }

                    if (oFound != -1)
                        OPEN.RemoveAt(oFound);
                    if (cFound != -1)
                        CLOSED.RemoveAt(cFound);

                    OPEN.push(node_successor);
                }//foreach

                CLOSED.push(node_current);
            }//while

            Node p = node_goal;
            while (p != node_start.parentNode)
            {
                SolutionPathList.Insert(0, p);
                p = p.parentNode;
            }
            Steps.Add(SolutionPathList);

            //return (WayPt);
        }//Waypoint Method

    }
}

