import java.util.ArrayList;
import java.util.List;

public class Dizikuyruk {
    private List<String> oncelik1 = new ArrayList<>();
    private List<String> oncelik2 = new ArrayList<>();
    private List<String> oncelik3 = new ArrayList<>();


    public void ekle(Musteri musteri,int oncelik){

        if (oncelik==1){
            oncelik1.add(musteri.getAd());
        }else if(oncelik==2){
            oncelik2.add(musteri.getAd());
        }else if (oncelik==3){
            oncelik3.add(musteri.getAd());
        }
    }
    public void siradanCikar(){
        if(!oncelik1.isEmpty()){
            oncelik1.remove(0);
            System.out.println("birincil oncelikli siradan cikarildi..");
        }else if(!oncelik2.isEmpty()){
            oncelik2.remove(0);
            System.out.println("ikincil oncelikli siradan cikarildi..");
        }else if(!oncelik3.isEmpty()){
            oncelik3.remove(0);
            System.out.println("ucuncul oncelikli siradan cikarildi..");
        }
        else {
            System.out.println("kuyruk bos.");
        }
    }
    public void listele(){
        System.out.println("oncelikli 1 liste:");
        for (int i =0;i<oncelik1.size();i++){
            System.out.println(oncelik1.get(i));
        }
        System.out.println("oncelikli 2 liste:");
        for (int i =0;i<oncelik2.size();i++){
            System.out.println(oncelik2.get(i));
        }
        System.out.println("oncelikli 3 liste:");
        for (int i =0;i<oncelik3.size();i++){
            System.out.println(oncelik3.get(i));
        }
    }
}
